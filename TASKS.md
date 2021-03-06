Game Engine Tasks
=================

Todo
----

- [] - Refactoring of the way drawables are loaded from .anim files
	- [] - Make IGameDrawable Files require to provide a XmlToDrawable static method which allows it to be loaded from an xml file.
	- [] - Refactoring of the xsd. Animations->Drawables. <DrawableInstance><Animation>...</Animation></DrawableInstance> etc..
	- [] - Change all .anim file extensions to .draw extensions
	- [] - Remember that refactoring will break other peoples code so we need some way of transitioning this.
- [] - To Consider. Allow for specifying an Entity does not need to be added to the Collider (Example LightSource does not need to be added).
- [IP] - Collision detection between entities.
		 - The current works relatively well but some form of optimizations need to be performed to reduce CPU usage.
		 - Some forms of physics to allow proper reaction would be better.
- [] - Allow specifying of constants in some kind of file which can then be referenced in tiled through some convention such as %SOME_KEY%
- [] - Write some unit tests.
- [] - Allow overriding of some Entity value DefaultOrigin which allows the automatic loader to determine how much displacement to apply.
- [] - Fix bug in Animation class related to 'IsFinshed()'. Make IGameDrawable classes expose some form of GetCurrentState() enumeration.
- [] - Develop an animation creator/editor so that it doesnt have to be manually specified. Probably best to create this in a seperate repository. Either use QT in order to learn it or some form of WPF.
- [] - (Bug) Entities at certain zoom levels show pixels from outside their source frame (example Bat and Tree2)
- [IP] - Add some new monsters to the map, maybe something that walks - in which case start implemeting A* algorithm. Check repository used by behindcurtain.
- [Done] - Refactoring of DebugInfo. Watches should be contained within DebugInfo and should be stored as a Dictionary rather than using HardCoded properties which can be very unwiedly and hard to mantain.
- [Done] - Loading of Items from storage. JSON is probably the nicest format in this case, but XML should also be considered.
- [Done] - LoadAnimationXML should be moved to the DrawableSet class and changed to something like LoadDrawableSetXML().
- [Done] - Improve HashList DrawDebugInfo drawing code in terms of performance (currently very ineffecient).
- [Done] - Add DrawCenteredString method to SpriteBatchExtensions.
- [Done] - Continue working on DrawDebugInfo code for HashList. Rectangles should be color coded according to how many entities reside in them. It should also should its index and the count of its entities.
- [Done] - Add ability to add 'meta-properties' to Maps by placing a . in front. Example '.MyRandomProperty'. Which WONT be reflected to the Entity conversion.
- [Done] - Fix on Map Zone Hit.
- [Done] - Create PulseLightSource which derives from LightSource. Or allow a specification of a Pulse value in LightSource.
- [Done] - Add Initialise(TeeEngine engine) method.
- [Done] - Automatic conversion of TiledObjects to Entities by TeeEngine. Started work in LoadEntities in TeeEngine.cs
- [Done] - Automatic loading of Entities based on the specified Type. Reflection can then set the properties.
- [Done] - Create some Model View Controller paradigm for controlling Maps with backend code. Specify some 'Model' class to handle a Map in its Property metadata.
- [Done] - Rename 'Objects' Property in TiledObjectLayer to 'TiledObjects'
- [Done] - Add some notes in the wiki about requirements in TiledMaps such as specifying the Content property for each tileset added.
- [Done] - Find a way to place tmx files and tsx files in seperate folders. Problem currently is that they need to reference Content.
- [Done] - Remove EntityLoadCallback from LoadMap. Instead specify a MapLoaded event in TeeEngine that is called everytime a map is loaded using LoadMap.
- [Done] - Investigate imported tileset support (trx files) so that properties etc can be shared amognst multiple maps.
- [Done] - AI for bats. Bats do not really need to make use of A*.
- [Done] - better entity integration in map loading. Finished with LoadEntityCallback in LoadMap method (?)
- [Done] - Add transition capabilities in maps. Example_Map->Cave_Example
- [Done] - Bug when adding more tilesets after the custom sized treetop tileset. This is because the tilewidths do not exceed the images width and height perfectly as expected.
- [Done] - Change *KeyboardExtensions* class to make use of a Dictionary&lt;string, HashSet&lt;Keys&gt;&gt; which should be much faster and scalable.
- [Done] - Implement extended IntersetsWith function. IntersectsWith(Entity entity, GameTime gameTime, string thisGroup=null, string entityGroup=null)
- [Done] - Refactoring of TiledMap namespace to support ILoadable and gracefully transition between Map changes.
- [Done] - Change SpriteBatchExtension 'DrawMultiLineString' to automatically convert strings to mutltiple lines given some maxline length
- [Done] - Create virtual Entity method. ShowDebugInfo (or something like that) that is shown by the TeeEngine when required.
- [Done] - Add support for specifying tile layer 'Color'. For example, the cliff layer could be set to some light gray to give the feeling of distance.

QuadTree / HashList stuff
------------------------

- [Done] - Create branch for trying out a HashList implementation which could possibly be superior and less buggy than the current QuadTree implementation.
- [] - Consider converting BoundingBox back to normal Rectangle and making the QuadTree using those instead. Faster.


Must Have
---------

- [Done] - The ability to draw and render a 2D top down game world
- [Done] - an entity framework
- [Done] - an animation framework
- [Done] - a post-rendering shader framework
- [Done] - the ability to load from tiled maps (prevents from having to develop a map creation application)

Should Have
-----------
- [] - Ability to run in full screen mode. Currently this does not work as expected.
- [IP] - The ability to allow the player to change between various armor sets, weapons, hair styles, gender etc... just like an RPG.
- [] - NPC Interaction (scriptable with dialog text)
- [Done] - the ability to zoom in and out (scale)
- [Done] - an effecient collision detection engine (QuadTree)
- [] - a per entity / per tile shader framework
- [] - an application to be able to design animations and speficy frames (however try look into alternatives such as texture compressor)
  - try out QT in the case where an application will be designed from scratch
- [Done] - Map scripting in the form of python, lua or external C#. Need to do research to see which one is the best to use. Make sure to consider **performance**, **ease of use**, **support** and **stability of package**.
       - example: http://www.gamedev.net/page/resources/_/technical/game-programming/using-lua-with-c-r2275 *LUA*
       - example: http://mail.python.org/pipermail/pythondotnet/2003-November/000037.html *PYTHON*
       - example: http://stackoverflow.com/questions/826398/is-it-possible-to-dynamically-compile-and-execute-c-sharp-code-fragments *C#*
- [] - Animated tile support. This is currently a bit hard in Tiled, but hopefully [support for specifying animated tiles will come soon](https://github.com/bjorn/tiled/issues/57#issuecomment-16699982)
- [Done] - Composite drawing support in entities (allowing entities to make use of more than one animation at one go)
- [Done] - Detailed diagnostic information in terms of performance counters
- [Done] - the ability to reset animations

Nice to Have
------------

- [] - Support for loading Image layers specified in tiled files.
- [] - zlib compression support in tmx files
- [] - the ability to pause and resume the state of the world from within the engine itself
- [] - the ability to run on MonoGame
