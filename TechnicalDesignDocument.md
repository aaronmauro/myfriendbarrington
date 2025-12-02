<div align="center">
	 <H1>Technical Design Document</H1>
</div>

<H2>Scene Settings</H2> 

All Prefab: 

Z: Around -1 to 1

Set all Hazards and playable objects set to Z Position 


<H2>Player</H2>

Player Settings: 

In-engine specifications: 

Universal gravity: -9.81 

Capsule collider 

Transform: 
		Scale: X=1, Y=1.5, Z=1 
		
Rigidbody: 

	Mass: 6.7
	
	Constraints: 
	
	Freeze Position:  Z 
		
Player Inspector Value Script: 

	Speed Acceleration: 50 NEW 8 
	
	Speed Max: 10
	
	Ground Drag: 0 NEW 2 
	
	Jump: 90 NEW 5.6
	
	Jump Cooldown: 0.1 NEW 0.5

	Max Jump Timer 0.4

	Jump Force Increase 0.39 
	
	In Air Boost: 0.75

	Grapple

	Pull Speed: 3.5

	Stop Distance: 4 

	Hook Force: 25  


Player Code:

	Void movePlayer()
	{

	This script controls a player character in Unity, handling movement, jumping, ground detection, invincibility, and interactions 
	like being pushed or floating in bubble streams. It uses Unity’s physics system 	
	(Rigidbody) and the new Input System (InputActionReference) for player input.
	
	
	
	Get axis horizontal: A and D
	
	Move player by force  
	
	Jump Key Code: Space
	}

Player Controller:

	JumpAction = A Button || SpaceBar 

	MoveLeft/Right Action = Left JoyStick || A/D

	Interact/Talk = X || E 

	HookAction = B || E 

	

<H2>Mechanics</H2> 


Sticky Hand Code:

	This script implements a grappling hook mechanic for a player in Unity. 
	It allows the player to shoot a hook toward nearby grapple points, pull themselves toward the hook, 
	and automatically retract or destroy the hook under certain conditions.

	Public void Stickyhand()
	{
	Getrigidbody 3D 
	
	On trigger of interact button 
	
	Create line rigidbody 
	
	Get player position 
	
	Move player from positionX & positionY
	
	Press Hook Action  to Release Hook 

	Press Hook Action to Remove Hook 

	In Grapple Range Enable Hook 

	Else Hook Disabled 
	
	Destroy line 

	Pull Speed: 0.1

	Stop Distance: 4

	Hook Force : 25

	OnCollision with Grapple Point 
	Enable Grapple Hook 

	Hook Code:
	
	{	
		Create hook Line render 
		
		Check Grapple Layer 
		
		OnCollision on Grapple Layer Intiate Pull 

		If hook Not In Range/Does not Latch 

		Destroy Hook

	}

	Grapple Point Code: 

	{
	
	Player Tag On collision With Grapple Point 

	Enable Grapple Hook 

	Player Press HookAction to Use Hook In range 

	Hook Enables When in Range 

	Activation Range = 20f

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
SoundWaves:

	{
		Get SoundwavePrefab

		RespawnPointTime 

		DestroyDistance 

		TravelDistance 

		Public enum Going Direction 

		{
			All Directions 
		}

	  private void soundWaves()
	  
	  {
  
      _soundWavesObject = Instantiate(soundWavesPrefab, transform.position, Quaternion.identity);
      _soundWavesCollider = _soundWavesObject.GetComponent<Collider>();
      if (_soundWavesObject == null)
      {
          isSpawn = false;
      }
	  
	  }
	  
		switch (dir)
 		{
     case GoingDirection.isRight:
         return Vector3.right;
     case GoingDirection.isLeft:
         return Vector3.left;
     case GoingDirection.isUp:
         return Vector3.up;
     case GoingDirection.isDown:
         return Vector3.down;
     default:
         return Vector3.zero;
		 
 		}
		
		moveSoundWaves()

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

<H2>Managers</H2>

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

<H2>Unity Program Folder:</H2>

Scripts Folder:

	Managers Folder:

		GameManager 
	
		Button Folder:
		
			Button Manager
			
			Button Input Return Script

		Audio Manager:

			Audio Class 

			Audio Manager 

		Scene Script: 

			Change Scene

			Scene Manager

		Text Script: 

			TextManager 

			TextScript 

		Video Script:

			VideoManager 

			VideoData 
		
			
	Mechanic Folder:

		BubbleScript: 

			Bubbles

			BubbleSpawnPoint 

			BubbleStream 

		GrapplingHook:

			Grapple 

			GrapplePoint

			Hook 

		PlatformMechanic: 

			BreakingBlock 

			BreakingBox

			Bush 

			Lever 

			MovingBlock

			Pillow 

			PillowFort 

			Rift 

			SoundWaves  

		PlayerMechanic: 

			Damage 	

			InteractObject 
			
		
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

		Cloud_1 

		Cloud_2 
		
		
	3DLevel2 
	
		Trees.Obj

		Banana_1 

		Glassshards

		snake
		
	3DLevel3 
	
		pirate-ship.Obj

		anchor 

		Bed 

		Flag

		Mast 

		Pillow 

		SteeringWheel
		
	Environment 
	
	Obstacle
	
	CharacterModel
	
		BarringtonModel.Obj
		
		ParrotPete.Obj
		
		FelliniFerret.Obj
		
		MarvinMonkey.Obj

		StickyHand 
		
	
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


Scenes Folder:

	Lvl 1
	
	Lvl 2
	
	Lvl 3
	
	LevelDesign

	Test
	
	Video
	
	Cinematest


<H2>Access GitHub:</H2> 

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
 
<H2>Software/File Type</H2>   

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

https://unity.com/cn/releases/editor/whats-new/6000.0.53f1 

Inkle

https://www.inklestudios.com/ink/

Cinemachine 3.1.4 

https://unity.com/features/cinemachine 

GitHub Desktop 3.5

https://desktop.github.com/download/ 

Blender 4.5.4 LTS   

https://www.blender.org/download/

Procreate 5.4.2 LTS   

https://procreate.com/ 

Da Vicini Resolve 20.2.1  

https://www.blackmagicdesign.com/ca/products/davinciresolve 

Canva:   

https://www.canva.com/ 

Wix:   

https://www.wix.com/ 

FMOD 2.03 

https://www.fmod.com/docs/2.03/studio/welcome-to-fmod-studio-new-in-203.html 

Figma: 

https://www.figma.com/ 

Microsoft Planner: 

https://planner.cloud.microsoft/ 

Discord: 

https://discord.com/ 

Google Suites: 

https://workspace.google.com/intl/en_ca/ 

<H2>System Requirement</H2>

| Operating system | Operating system version | CPU | Graphics API | Additional Requiremnts |
| :--------------- | ------------------------ | :-: | ------------ | ---------------------: |
| Windows		   | Windows 10/11 Version    | x86, x64 architecture with SSE2 instruction set support, Arm64 | DX10, DX11, DX12 or Vulkan capable GPUs | Hardware vendor officially supported drivers |
																																																			


<H2>Resources</H2>   

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

https://www.inklestudios.com/ink/web-tutorial/

To learn how to code scripts in Inkle 
