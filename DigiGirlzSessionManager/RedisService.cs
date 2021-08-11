using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiGirlzSessionManager
{
    public class RedisService
    {
        private readonly AppSettings _appSettings = null;
        private readonly ConnectionMultiplexer _connection = null;
        private readonly IDatabase _cache = null;
        private readonly string _roomKey = "rooms";

        public RedisService(AppSettings appSettings)
        {
            _appSettings = appSettings;
            _connection = ConnectionMultiplexer.Connect(appSettings.RedisConnection);
            _cache = _connection.GetDatabase();
        }

        public async Task Reset()
        {
            await _cache.SortedSetRemoveRangeByScoreAsync(_roomKey, double.NegativeInfinity, double.PositiveInfinity);

            for (int i = 1; i <= _appSettings.NumberOfRooms; i++)
            {
                await _cache.SortedSetAddAsync(_roomKey, i, 0, CommandFlags.None);
            }
        }

        public async Task<List<Room>> GetAllRooms()
        {

            return (await _cache.SortedSetRangeByScoreWithScoresAsync(_roomKey))
                .Select(x => new Room(_appSettings)
                {
                    RoomId = int.Parse(x.Element),
                    Count = x.Score
                }).ToList();
        }

        public async Task<Room> GetNextRoom()
        {
            var nextRooms = (await _cache.SortedSetRangeByScoreWithScoresAsync(_roomKey, order: Order.Ascending, take: 1));

            while (!nextRooms.Any())
            {
                await Reset();
                nextRooms = (await _cache.SortedSetRangeByScoreWithScoresAsync(_roomKey, order: Order.Ascending, take: 1));
            }

            var next = nextRooms.First();
            var room = new Room(_appSettings)
            {
                RoomId = int.Parse(next.Element),
                Count = next.Score
            };

            await _cache.SortedSetIncrementAsync(_roomKey, next.Element, 1);
            return room;
        }
        
    }
}
