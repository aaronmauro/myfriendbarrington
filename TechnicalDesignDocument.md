Technical Design Document   

Scene

All Prefab: 

Z: Around -1 to 1

Player

Player Settings: 

In-engine specifications: 

Universal gravity: -9.81 

Capsule collider 

Transform: 
		Scale: X=1, Y=1.5, Z=1 
		
Rigidbody: 

	Mass: 4 
	
	Constraints: 
	
	Freeze Position:  Z 
		
Player Script: 

	Speed Acceleration: 45 
	
	Speed Max: 7 
	
	Ground Drag: 0 
	
	Jump: 25 
	
	Jump Cooldown: 0.1 
	
	In Air Boost: 1 


Player Code:

	Void movePlayer()
	{
	
	Get axis vertical: W
	
	Get axis horizontal: A and D
	
	Move player by force  
	
	Jump Key Code: Space
	}

Mechanics 


Sticky Hand Code:

	Public void Stickyhand()
	{
	Getrigidbody 3D 
	
	On trigger of interact button 
	
	Create line rigidbody 
	
	Get player position 
	
	Move player from positionX & positionY
	
	On release of pressing E 
	
	Destroy line 
	}

Interact Button: 

	{
	
	Using TMP
	
	Text message on Scene
	
	Scale: 
	
	X: 0.1 
	
	Y: 0.1 
	
	Z: 0.1
	
	Text: E to Interact
	
	Font Size: 36
	
	Font Style: None
	
	Position: 
	
	PosX: 1.577 
	
	PosY: 0.785 
	
	PosZ: 0
	
	Getting Key Code (Player Input): E
	
	}


Pushing Mechanics:

	{
	Box Collider:
	
	Size: 
	
	X:3 
	
	Y:2 
	
	Z:2
	
	Two box Collider, one for staying above ground, one for trigger
	
	On Trigger enter and pressed input button: isPressed()
	
	Transform Object: transform to Interact Box Location
	}

Quick Time Events:

	{
	
	Screen Size: Scale With Screen Size
	
	RawImage Size: Scale with Canvas size
	
	Button
	
	Scale: 
	
	X: 1 
	
	Y: 1 
	
	Z: 1
	Source Image: UISprite
	
	On Click () 
	
	Run ButtonReturn Script
	
	onPress( int input values )
	
	Video Manager
	
	Button Manager
	
	}

Respawn:

	{
	
	Public Void respawn

	isInvincible != true
	
	Player position = spawn point position
	
	If Player on ground and Player danger box collider didn’t trigger any hazard
	
	Spawn point position = player position
	}

Bush:

	{
	
	Box Collider:
	
	Size: 
	
	X: 1.21 
	
	Y: 1 
	
	Z: 4.23
	
	Getting Player Collider
	
	Exclude Layer: Sound Wave Layer
	
	}

Sound Wave:

	{
	
	Movement Speed: 2
	
	Movement Direction: Up - Down / Down - Up / Left - Right / Right - Left 
	
	Box Collider: 
	
	Size:
	
	}

Rift:

	{
	
	Rift Size:
	
	X: 1
	
	Y: 0.2
	
	Z: 1
	
	Teleport Location A
	
	Teleport Location B
	
	OnTriggerEnter: Teleport()
	
	}

Platform:

	{
	
	Moving Platform

	Gameobject get distance A 
	
	Get distanceB 
	
	Transform position from A to B 
	
	Transform position speed * Time.deltaTime 
	
	}

Bubble Platform:

	{
	
	Bubble Spawn Points: Transform position
	
	Bubble Respawn Time: 3
	
	Bubble Prefab - Asset > Bubbles
	
	}

Slippery Platform:

	{
	
	Material on game object to increase friction by 20
	
	Bounciness increased to 0.75 
	
	}

Sticky Platform:

	{
	
	Material on game object to decrease friction to 0 
	
	}

Lever:

	{
	GetGameObject.Player
	
	Oncollision Player && Lever 
	
	MoveDirectionLever 
	
	Lever = Active 
	
	SetPlatformMovement = True 
	
	Call Platform Prefab 
	
	}

Managers

Game Manger:

	{
	
	Tracks player and spawn point positions
	
	Updates spawn point when player is grounded and safe
	
	Respawns player at last safe point if backtoSpawn is true and not invincible
	
	Controls invincibility and danger detection states 
	
	}

Audio Manager:

	{	
	
	Tracks game audio and where all audio will update and recall from 
	Changeable audio settings for SFX and background audio 
	Will be called when different SFX are being played from player, npc, objects 

	VARIABLE musicSource
    VARIABLE sfxSource
    VARIABLE ambientSource

    // Audio clip library
    DICTIONARY<string, AudioClip> soundLibrary


    FUNCTION Awake()
        // Ensure only one AudioManager exists
        IF Instance == NULL THEN
            Instance = THIS
            DontDestroyOnLoad(this)
        ELSE
            Destroy(this)
        END IF
    END FUNCTION

  	Plays background music (loops)
    FUNCTION PlayMusic(trackName)
        clip = soundLibrary[trackName]
        IF clip == NULL THEN
            PRINT "Music track not found!"
            RETURN
        END IF

        musicSource.clip = clip
        musicSource.loop = TRUE
        musicSource.Play()
    END FUNCTION
	}

Button Manager:

	{
	
	List<GameObject> buttons
	
	bm.buttons.Add(gameObject)
	
	onPress()
		vm.videoCount = buttonValues - 1
		Remove from list
		False to active, loopVideo
		
	}

Video Manager:

	{

	Where videos will be stored and looped from 
	
	Where live cutscenes will be played from 
	
	playVideo(video Name)
	
	checkVideoStatus()
	
	CheckVideoLoop()
	
	}

Scene Manager:

	{
	Where each level and scene will be stored and called from 
	
	VARIABLE currentSceneName

    Called when the game starts
    FUNCTION Start()
        currentSceneName = GetActiveSceneName()
    END FUNCTION

    Loads a scene by name
    FUNCTION LoadScene(sceneName)
        IF sceneName != currentSceneName THEN
            ShowLoadingScreen()
            AsyncLoadScene(sceneName)
        END IF
    END FUNCTION
}

Dialogue Manager:

	{
	NPC Script will follow a set of lines 
	Will have tree branches for different options 
	Leading to different outcomes 

    VARIABLE dialogueQueue = new Queue()
    VARIABLE currentSpeaker
    VARIABLE isDialogueActive = FALSE


    FUNCTION StartDialogue(dialogueData)
        isDialogueActive = TRUE
        Show(dialogueBox)
        
        dialogueQueue.Clear()
        currentSpeaker = dialogueData.speakerName

        FOR EACH line IN dialogueData.lines
            dialogueQueue.Enqueue(line)
        END FOR

        DisplayNextLine()
    END FUNCTION	
	}

Camera Manager:

	{

	Camera Transform: Pos: X=0, Y=1.3, Z=-12 Rot: X=8
	Main Camera size = 
	Dialogue Camera size = 
	Cutscene Camera size = 
	CLASS CameraManager

    References to different virtual cameras / angles 
    VARIABLE mainCamere 
    VARIABLE dialogueCamera
    VARIABLE cutsceneCamera

    FUNCTION Start()
        // Set initial camera
        activeCamera = mainCamera
        SetCameraPriority(mainCamera, 10)
        SetCameraPriority(dialogueCamera, 0)
        SetCameraPriority(cutsceneCamera, 0)
    END FUNCTION
}

Unity Program Folder:

Player Folder:

	Player Script
	
		DangerDetect 
		
		Player 

Script Folder:

	Manager Folder:
	
		Button Folder
		
			Button Manager
			
			Button Input Return Script
			
		NPC
		
			NPC System
			
			Next Dialogues
			
Video Script

			Video Manager
			
			VideosData 
			
		Game Manager
		
		Audio Manager
		
	PrefabScript
	
		Bubble Script
		
			Bubbles
			
			Bubble Spawn Point
			
		Bush
		
		Interact Object
		
		Sound Waves
		
Breaking Block

		Moving Block
		
		Rift
		
	Damage
	
	Grapple 
	
	Hook

Prefab Folder:

	Materials 
	
		Boundary 
		
		Rift 
		
		BreakingPlatform
		
		Bush
		
		Hazard 
		
		TestingRiftMaterials 
		
	PhysicsMaterials 
	
		HighFriction
		
		LowFriction 
		
		Slip
		
		Hook
		
	Objects
	
		Bubbles 
		
		SoundWave
		
	Platform 
	
		BreakingPlatform
		
		MovingPlatform
		
		Pillow

	Player
	
		Player
	
	Mechanics
	
		Bush
		
		Hazard 
		
		MovableBox
		
		NPC
		
		RiftpointA 
		
		RiftpointB
	
2D-Art Folder:

	Environment
	
		Background 
		
	UI
		Buttons 
		
		Font
		

3D-Art Folder:

	3Dlevel1 
	
		Alphabet_blocks
		
		Bubble_bottle
		
	3DLevel2 
	
		Trees.Obj
		
	3DLevel3 
	
		pirate-ship.Obj
		
	Environment 
	
	Obstacle
	
	CharacterModel
	
		BarringtonModel.Obj
		
		ParrotPete.Obj
		
		FelliniFerret.Obj
		
		MarvinMonkey.Obj
		
	
Audio Folder:

	Background Audio	
	
		Background Music 
		
	Environment Audio
	
		  Rift Audio 
		  
		SoundWave Audio
		
	Player Audio
	
		Walking Audio
		
		Jumping Audio
		
		Damage Audio
		
		Talking Audio 
		
		Running Audio 
		
	NPC Audio
		Talking Audio
	SFX
	
		Glass Audio 
		
		WhoopieCushion Audio
		
		StickyHand Audio 
		
		BubbleBridge Audio
		
	Video Audio	
	
		LiveCutSceneAudio 1
		
		LiveCutSceneAudio 2
		
		LiveCutSceneAudio 3
		
Video Folder:

Ads1 Testing

	StickyHandVideo
	
Ads2 Testing

	RoxaneCutscene
	
Testing

	TestingVideo

Scenes Folder:

	Lvl 1
	
	Lvl 2
	
	Lvl 3
	
	LevelDesign

	Test
	
	Video
	
	Cinematest


Access GitHub: 

School Computer using GitHub:  

Find GitHub Desktop on desktop https://github.com/aaronmauro/myfriendbarrington   

Login to your GitHub Account  

Select "myfriendbarrington" and select "Clone myfriendbarrington"  

Optional but suggested (remember/change the Local path to a place you will remember)  

Click "Clone" 

Click on "Current branch" on top left, click on branches corresponding to your role. 

Click on "Show in Explorer"  

Click on "My Friend Barrington"  

Click on "Assets"  

Click on "Scenes"  

Select any Unity scene file ending with ".unity" (ex. Lvl1.unity, test.unity)

Wait and Have Fun Good Luck  


Personal Computer using GitHub:  

Follow the first 5 steps ONCE (don't repeat this step each time, unless something happened)

Remember your file location is very important on personal computer  

On top of the GitHub Desktop find "Fetch origin" and click on it every time you wanted to access to the file (updating the file each time you use)  

Follow step 6-11 if you wanted to access to the game  

if anything happen ping any programmer  

For Commit and Pushing to GitHub:  

Before pushing to GitHub server, remember to check and save in Unity  

Open GitHub Desktop that is already on your screen  

On the bottom left find text box "Summary (required)" Write something (ex. Updated on Frog Enemies, Just anything for other people to see, keep it simple)  

For the text box below, "Description", this is optional for people want to write more detail  

Click on the blue button "Commit to main"  

Find "Push origin" button and click SUPER IMPORTANT  

You are free to close the GitHub Desktop and Unity  

Additionally:  
If you want to find the cloned file, Open GitHub Desktop, Find and click on "Show in Explorer" / click on "Repository" and click on "Show in Explorer" / Ctrl + Shift + F 

https://github.com/aaronmauro/myfriendbarrington/branches/all

There will be 3 branches use correct department branch, if pushing to main let Programmer team know 

When merging or updating branch make sure each branch has been pushed before merging to main 
 
Software/File Type   

File Naming Conventions: 

PascalCase 

Short and Concise Names 

Address What the asset is 

Any object the player interacts with should be tagged with PlayerInteractable 
 
Character/Object Name_Location_Material/AssetType 

Character/Object Name_Location_Material/AssetType_PlayerInteractable 
 
_texture 
 
_3D_Model 
 
Narrative: Narrative .txt files for documents/scripts  
  
2D: Procreate 300 DPI (Procreate 5.4.2)  
  
3D: Steam .obj File (4.5.3 LTS Steam)  
3D Printing STL file type   
FBX Animated Objects  
Video: video mp4/ link file type   
Unity 6000.0.53f1  
Cinemachine 3.1.4 
GitHub Desktop 3.5   
Blender 4.5.3 LTS   
Procreate 5.4.2 LTS   
Da Vicini Resolve 20.2.1  
Canva   
Wix   
FMOD 2.03 

Resources   

https://learn.unity.com/  

To learn unity functions and tools to help with problems when using unity

https://docs.unity3d.com/6000.2/Documentation/ScriptReference/Resources.html

Script functions for coding in C# and unity     

https://docs.unity3d.com/6000.2/Documentation/Manual/animeditor-AnimatingAGameObject.html#:~:text=You%20can%20animate%20any%20property,keyframes%20while%20in%20Record%20mode.

Animating Gameobjects in Unity3D   

https://stackoverflow.com/questions

Looking for potential similar bugs that you may be facing when coding  

https://docs.github.com/en

Information on GitHub Usage and how to use    

https://unity.com/features/cinemachine  

Information on how to use cinemachine in the unity 3D project 

https://www.youtube.com/@unity  

Tutorial videos and beginners guide to working in Unity 

https://learn.microsoft.com/en-us/dotnet/csharp/  

To learn C# language for Unity

https://docs.unity3d.com/Packages/com.unity.cinemachine@3.1/manual/setup-follow-camera.html

Cinemachine camera Follow target document 
