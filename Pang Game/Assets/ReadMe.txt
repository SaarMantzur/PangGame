I have choose to implement:
 
1. Architecture designed with MVC paradigm:
    	To my opinion this the correct way to build this kind of gamming app, and that's the way I worked until today.
    	Looking at the code will look like there is one main class: "GameControllerManager", who represents the Controller,
    	which holds instance of the non monovehaviour class "CoreGameFlow", who represents the model,
    	and holds the "ViewManager" class who represents the view manager.
    	The view or model do not communicate with each other or hold the instance of the controller. All the communication
    	needed is being transferred with events who are listened in the controller that operates the matching functions in
    	both model and view.
 
2. Three or more distinct consecutive levels, with increasing difficulty:
    	The right way to create this kind of application is creating something maintainable
    	for the long range. To my opinion, scripting an easy way to add more levels and edit the current levels later
    	is the correct way rather than creating a scene full of prefabs who cannot be easily maintained later, even if
    	it takes much less time.
    	I have added 4 Levels. The level creation is dynamic and takes place in the Model.
    	Data for the level is created and later being read by the View class who translates it to graphic balls.
    	The ultimate way to implement this, to my opinion, is a json \ yaml file delivered from server every time is necessary.
    	That way, there is no need to update users applications, just the data stored in the server, every time a new level is added.
    	I didn't do this because of time limit and assuming this is to far from what this task is about.
 
3. Custom visuals and shaders:
    	The player character (texture and animations) and the background of the game,
    	are taken directly from the original game. I found them as part of bigger images and then edited them in photoshop in order
    	to reach this level. Unfortunately, I never found the projectile.
    	I gained a lot of experience in these kind of stuff and wanted the app to look nicer using abilities I own.
 
4. Custom soundtracks and SFX:
    	I have a simple asset of sound effects I worked with, in many projects before, so I thought it would be a nice
    	addition for the task.