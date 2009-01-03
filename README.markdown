The two core libraries are CardManagement and ColorettoLib. To open them in MONO DEVELOP, the MONO-DEVELOP solution file is '~/CardManagement/CardManagement.mds'.

CardManagement.csproj
- The CardMangement doesn't really have much logic in it. It basically contains enough logic to create, shuffle, and handle basic operations you'd expect from a card library. The interesting file to look at (and probably the best place to start) is ~/CardMangement/Coloretto/ColorettoDeckProvider.cs.

It should be noted that every object is immutable. For example, adding, removing, or drawing methods returns a new deck. 

ColorettoLib.csproj
- This project contains all of the logic needed for the game. It was written and unit tested before the UI was even thought about. The interesting file here is ~/ColorettoLib/Game/ColorettoGame.cs and all of the actions availble each being a type of their own found in ~/ColorettoLib/Actions/*.cs, although it might be even more helpful to look at some of the unit tests.

Like the CardManagement, everything about ColorettoLib is immutable. For example, applying an action on the game causes a new game to be returned.

Have fun!
