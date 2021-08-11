# DigiGirlzSessionManager

This app was written to help with a Microsoft DigiGirlz event. 

## what it does

They had a few hundred students attending an online Teams meeting. However, they needed to break the main group up into breakout rooms. At the time of writing this the Teams breakout room feature did not support these many attendees. To that end they manually created 51 Teams meetings. One Auditorium meeting where all the attendees would go for the large eyes forward presentations, and 50 break out teams meetings that the attendees would go to for the smaller breakout rooms. 

In the morning the app is set to auditorium mode, the app sent all attendees to the auditorium teams meeting. 5 minutes before the students needed to switch to breakouts the app was toggled into breakout mode. Now when a student hit the app, they would be redirected to the breakout room with the fewest number of people already sent to it. 5 min before the end of the breakouts the app was toggled back into auditorium mode. Attendees were asked to disconnect from the breakout meeting and go back to the website the redirected them back to the auditorium room. 

This ensured that sudents only ever needed 1 link (the link to the app) and they would get sent to the right place depending on where we were in the agenda. This also ensured a even distribution of students to brekout rooms, with out them needing to be pre-assigned before the event. 

## how it works

A cache of how many students are sent to each room is kept in Redis. The app is a simple ASP.NET razor pages website. I hosted both in Azure for the event. 

### the urls

- By default attendees will only see the index page. This figures out where they should go, increments the proper counter and sends them off. 
- Admis can use the admin page to keep an eye on the number of people sent to each breakout room, and to check the URLs for each of the breakout rooms. 
- Admins can use the reset page to reset the redis cache, for example for a multi day event. 

### the configuration

- RedisConnection – the connection string to your redis cluster
- RoomURLBase – for the breakout rooms we used https://aka.ms/BreakoutRoom## were ## was the number of the room (i.e. https://aka.ms/BreakoutRoom01, https://aka.ms/BreakoutRoom22, etc.) we used the following https://aka.ms/DigiGirlzRoom{0:00}
- RoomNameBase – for the breakout rooms same idea as above, just creates a human name for the room instead of the URL. We used the following DigiGirlz Breakout Room {0:00}
- NumberOfRooms – the app will create room 1 through whatever number you give here, for example we used 50, because we wanted 50 rooms. But you could use more or less. When you hit the reset URL it will query this value and prime Redis with the right keys. 
- Auditorium – this is the urn to the auditorium room. 
- AuditoriumOn – true/false – this toggles the app between auditorium mode and breakout mode
