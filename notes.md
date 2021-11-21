# Notes about this project (GO21 game off 2021)

## Resources
1. https://unityatscale.com/unity-version-control-guide/how-to-setup-unity-project-on-github/
2. https://www.studica.com/blog/how-to-setup-github-with-unity-step-by-step-instructions
3. https://thoughtbot.com/blog/how-to-git-with-unity
4. https://git-lfs.github.com
5. https://github.com/halak/unity-editor-icons
6. Blend tree: https://www.studica.com/blog/game-design-tutorial-blend-trees-unity
7. https://www.youtube.com/watch?v=m8rGyoStfgQ : How to Animate Characters in Unity 3D | Blend Trees Explained: One Dimensional
8. https://forum.unity.com/threads/how-can-i-play-an-animation-backwards.498287/ : Reverse animation
9. Most probably won't work reverse animation: https://www.youtube.com/watch?v=TqilaxE5N8Q
10. https://forum.unity.com/threads/how-to-handle-complex-unity-animator-controller.1110317/
11. https://unity.github.com
12. https://www.youtube.com/channel/UCYbK_tjZ2OrIZFBvU6CCMiA
13. https://www.reddit.com/r/unity_tutorials/comments/qfe24n/moverun_animation_plays_after_other_animations/
14. https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483
15. https://www.youtube.com/watch?v=E6A4WvsDeLE
16. [ X ] Bardent's State Machine video : https://www.youtube.com/watch?v=OjreMoAG9Ec&list=PLy78FINcVmjA0zDBhLuLNL1Jo6xNMMq-W&index=22
17. [ X ] Roundbeargames : https://www.youtube.com/playlist?list=PLWYGofN_jX5BupV2xLjU1HUvujl_yDIN6
18. [ X ] Single Sapling Games : https://www.youtube.com/watch?v=qc0xU2Ph86Q
19. [ ? ] samyam : https://www.youtube.com/watch?v=583R9LgRZPA
20. [ ? ] : https://www.youtube.com/watch?v=J5V8WtQdOzQ
21. [ ? ] GD Titans : https://www.youtube.com/channel/UCUJGE6eXB1OXPXbx4CXzTPA
22. [ ? ] 2.5D unity game : https://www.youtube.com/watch?v=wYulZiBKg-I&list=PLqqkaa8OrxkHayfk1DW1ayaVcajNBCpkf
23. [ ? ] 2.5D tutorial : https://www.youtube.com/watch?v=8Ln1cEfhmpo
24. [ ? ] DOTween : https://www.youtube.com/watch?v=Y8cv-rF5j6c
25. [ ! ] Ocean share : https://www.youtube.com/watch?v=FbTAbOnhRcI


## Links
1. _CANCELLED_  https://pawelgrzybek.com/unity-development-on-macos-with-visual-studio-code/ : unity development environment with vscode
2. https://www.youtube.com/watch?v=mFNrOGPVls0 : Making a Game in ONE Day (12 Hours) by Dani
3. https://www.youtube.com/watch?v=tysuunwI3oM : Total Animation Solution: How To Create And Manage Animations In Unity 2020
4. https://unity.com/how-to/beginner/10-game-design-tips-new-developers
5. https://thegamedev.guru/unity-performance/checklist/
6. https://www.gamedev.net/tutorials/programming/general-and-gameplay-programming/checklist-for-a-top-performing-game-in-2020-r5334/
7. https://www.reddit.com/r/unity_tutorials/comments/qw6njf/moving_platforms_for_2d_and_3d_games_with_custom/
8. https://www.reddit.com/r/unity_tutorials/comments/qozg0x/lets_take_our_knowledge_of_state_machines_to_next/ 
9. https://www.reddit.com/r/unity_tutorials/comments/qq4fpd/everything_about_particle_system/ 
10. https://www.reddit.com/r/unity_tutorials/comments/qq35xe/doors_in_unity_sliding_and_rotating_doors_make/
11. https://www.eventbrite.ca/e/gi-game-jam-fall-2021-tickets-198553166867
12. https://www.youtube.com/watch?v=kQ9-AFYV42Y
13. https://www.reddit.com/r/unity_tutorials/comments/qgoqg8/after_a_long_journey_weve_officially_started_the/
14. https://www.reddit.com/r/unity_tutorials/comments/qg4uis/unity_2021_object_pool_api_what_is_object_pooling/ : https://www.youtube.com/watch?v=zyzqA_CPz2E
15. 


## Tasks
1. [-] Movement controller
2. [-] Water for the base-0
3. [-] sound from ilham
4. [-] new animations from gökalp

## Tricks
- https://forum.unity.com/threads/trying-to-add-animation-events-to-a-fbx-files-animation-says-read-only-wont-save-events.342882/
> The clip duplication technique is very cumbersome.
With dozens of clips and the need to edit them from the program (Maya, Max, Blender etc ...) becomes some problems...
>
> The best solution is to insert the events in the animations at runtime, in the start call this simple function:
```
void AddEvent(int Clip,float time, string functionName,float floatParameter)
{
    anim = GetComponent<Animator>();
    AnimationEvent animationEvent = new AnimationEvent();
    animationEvent.functionName = functionName;
    animationEvent.floatParameter = floatParameter;
    animationEvent.time = time;
    AnimationClip clip = anim.runtimeAnimatorController.animationClips[Clip];
    clip.AddEvent(animationEvent);
}


For example:


Animator anim;

void Start () {
    anim = GetComponent<Animator>();
    AddEvent(1, 0.2f, "EmitProjectile", 0);
}

void AddEvent(int Clip, float time, string functionName, float floatParameter)
{
    anim = GetComponent<Animator>();
    AnimationEvent animationEvent = new AnimationEvent();
    animationEvent.functionName = functionName;
    animationEvent.floatParameter = floatParameter;
    animationEvent.time = time;
    AnimationClip clip = anim.runtimeAnimatorController.animationClips[Clip];
    clip.AddEvent(animationEvent);
}
```
- https://forum.unity.com/threads/how-to-add-animation-events-at-runtime.292549/
```
using UnityEngine;
using System.Collections;
 
public class _AAA : MonoBehaviour {
 
    private AnimationClip _aniclip;
    private AnimationEvent[] _aEvents;
    private Animator _myAnim;
 
 
    // Use this for initialization
    void Start () {
        _myAnim = GetComponent<Animator> ();
 
        _aniclip = _myAnim.runtimeAnimatorController.animationClips[0];
 
        _aEvents = new AnimationEvent[3];
        for (int i = 0; i < 3; i++){
            _aEvents[i] = new AnimationEvent();
            _aEvents[i].functionName = "aaa";
            _aEvents[i].time = .1f * i;
        }
//        Debug.Log (_aniclip.events.IsReadOnly);
        _aniclip.events = _aEvents;
        Debug.Log (_aniclip.events.Length);
 
    }
 
    // Update is called once per frame
    void Update () {
 
    }
 
    public void aaa(){
        Debug.Log ("+++aaaaa+++++");
    }
}
```
- https://www.obviousgravity.com/home/a-better-way-to-use-the-animator
> A better way to use the animator, all animations from any state

- https://www.reddit.com/r/unity_tutorials/comments/c0mskn/simple_games_to_makelearn_unity_checklist/
```
Simple games to make/Learn Unity checklist
Currently learning game development in unity and just wondering what types of simple games I can make without following youtube tutorials.
I am following a checklist I have made to learn c# and to create games in Unity:
Step 1:
Complete Brackeys "How to make a Video Game in Unity"
Step 2:
Complete Brackeys "How to program in C# - Beginner Course"

Step 3:
Complete SoloLearn Module 1: Basic Concepts
Step 4:
Complete Code Monkeys "Simple 2D Game in Unity:Snake
Step 5:
Complete SoloLearn Module 2: Conditional and Loops
Step 6:
Complete Brackeys"How to make an RPG in Unity"
Step 7: Keep working on SoloLearn until completion
Step 8: Make 5 different small games
Step 8.1: Endless runner
Step 8.2: Platformer
Step 8.3: Step 8.4: Step 8.5:
````
- https://answers.unity.com/questions/12522/checklist-in-preparing-a-game-for-public-release.html
```
Actually there are some certain standard tests you can and should check if you are about to release a project, over at msdn you can check the standard for windows games, Apple has the standards for iPhone games, and appup from intel has a validation test you may check too.
Basically this is what you need to check if you are going public:
1.-Your application should always run in a computer/browser/platform with the minimum requirements you have specified. (Set minimum requirements if you don't have them, don't assume everyone has the same platform specs)
2.-The interface should be easily readable and not cluttered, make sure all text is in the same language (this is easy to forget in a translated app) unless you have multilanguage capabilities.
3.-You should be able to play a complete test game session in your game (in a platform meeting the minimum requirements) without any major bugs stopping gameplay. (you wont believe how many people forget that before publishing a build)
4.-Beware of frame rate issues that may make the game unplayable.
5.-In the case your application should fail it should do it gracefully with an error message, not crashing the host computer/ browser. Avoid memory leaks and optimize your code as much as possible to avoid this.
6.-You should not use or display any material you don't have a signed permission or copyright to use. (parodies are sometimes included as fair use but not in all platforms) avoid exact clones of other software. (Even if its impressive) They would not pass validation in most platforms.
7.-Always include documentation on how to use the app. If possible include a simple tutorial.
8.-Make sure you have properly credited the game before release, you don't require a full credits screen (although is a good idea, specially if a team is involved), a simple splash screen logo or a text displaying the author or studio would do. Not doing so, may lead to IP problems later on.
9.-(imo) Beta test as much as possible as often as possible. positive feedback may turn a crappy game into a great one. Apply frequent updates based on this feedback even after is released.

```

## Unrelated
- https://www.youtube.com/watch?v=ZtqBQ68cfJc : 50 linux commands
- 
- https://www.fullhdfilmizlesene.com/film/the-lghuse-izlehd/,
- https://www.fullhdfilmizlesene.com/film/toprak-ve-kan-earth-and-blood-izle/
- https://www.fullhdfilmizlesene.com/film/kemiklerin-mirasi-the-legacy-of-the-bones-izle/
- https://www.fullhdfilmizlesene.com/film/rising-high-izle/
- https://www.fullhdfilmizlesene.com/film/torn-dark-bullets-izle/
- https://www.fullhdfilmizlesene.com/film/dunyanin-durdugu-gun-film-izle/
- https://www.fullhdfilmizlesene.com/film/olumcul-tuzak-the-hurt-locker/
- https://www.fullhdfilmizlesene.com/film/sonraki-talimatlari-bekleyin-await-further-instructions/
- https://www.fullhdfilmizlesene.com/film/suclu-den-skyldige/
- https://yabancidizi.vip/dizi/star-wars-the-clone-wars/sezon-7/bolum-9
- https://www.fullhdfilmizlesene.com/film/gunah-sehri-ugruna-olunecek-kadin-film-izle/
- https://www.fullhdfilmizlesene.com/film/47-ronin-izle/
- https://www.fullhdfilmizlesene.com/film/robot-kopek-a-x-l-izle/
- https://www.fullhdfilmizlesene.com/bilim-kurgu-filmleri-izle/shazam-filmhd-izle/
- https://www.fullhdfilmizlesene.com/film/adalet-birligi-apokolips-savasi-izle/
- https://www.fullhdfilmizlesene.com/film/lejyon-the-legion-legionnaire-s-trail-izle/