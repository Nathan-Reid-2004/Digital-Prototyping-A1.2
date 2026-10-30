### **Intro**



Our second prototype under NexGen Studios is intended to test whether or not momentum-based platforming can be fun when stuck in a looping environment, such as a halfpipe. We feel trapping the player within a limiting space would make for interesting gameplay. Rather than using momentum purely to build up speed or clear gaps in the environment, momentum will be used more as a defense mechanism. Players will need to use momentum to avoid hazards- our prototype is less of a game of speed, and more of a game of endurance. The objective of our test level is for players to survive as long as possible, using their momentum in smart ways while maintaining a safe speed and quick wits.
***



### **Technology**



Game engine: Unity (v2022.3.62f3)

Coding language: C#

Assets: Unity Asset Store

Repository host: GitHub

***



### **Objective Statement**



###### **Objective Statement Question**



Through this prototype we aim to answer the question:

***"Can momentum-based platforming be fun within an enclosed, looping environment?"***



While there are platformers that use momentum as a way to propel the player forward in a linear level, we want to see if using that momentum could still be fun inside of an enclosed environment. Here, instead of constantly moving forward, players would be moving back and forth, using the two sides of a halfpipe to propel them faster in either direction.



###### **Objective Statement Answer**



We have determined that yes, it is decently fun to perform momentum-based platforming in a confining, looping environment. Using the speed of the halfpipe and bouncing off of the walls is fairly satisfying. Yet, there is a certain intensity to each movement that comes with the sense of momentum and weight, in conjunction with the enclosed environment that limits the player's movement. The halfpipe environment gives ample opportunity to generate speed and get away with interesting maneuvers, but at the same time, it generates stress as a result of its space limitations. Although there is definitely some fine-tuning to be done in certain areas, we are confident our prototype could stand on it's own within a larger game.
***



### **Design Rationale**



If this prototype were to be part of a larger game, it would likely be part of a momentum-based platformer game, similar to something like the *Marble Blast* series, where players need to roll around environments using slopes/momentum to their advantage. This halfpipe could be implemented in the form of a level where players must avoid falling obstacles for a certain amount of time. Or, perhaps it could even be used as an "endless" or "arcade" mode, where players need to stay alive for as long as possible and the falling obstacles become more difficult to dodge as time goes on (the longer the player survives, the higher the score they get).

***



### **Mechanic(s)**



We will include a small jump function so that players can have a little more control over where they are going. Players will also have control over their direction using the A \& D keys, however this is limited due to the confining environment. With that said, players can "kick off" from the sides of the halfpipe using this movement, allowing for some extra maneuverability.



Within this halfpipe environment, balls are spawning from above and will drop down onto the halfpipe. The player must avoid these balls by rolling around, using the momentum and shape of the halfpipe to their advantage. If they are hit by one of these hazard balls, they will respawn in the middle of the playing field, where they are safe until they press the spacebar, which will reactivate their physics. This gives the player a bit of a buffer for when they are hit.

***



### **Progression**



As mentioned before, this is a game of endurance. As time goes on, more hazard balls will spawn, dropping down from above. These hazards will continue to bounce and roll around the halfpipe for sometime before settling down, so players must stay weary of where each hazard ball is. These hazards also do not despawn, meaning even when they do settle at the bottom of the halfpipe, the player must avoid that area altogether, adding to the tension.



The main progression here is the upping of stress as more hazard balls spawn, giving more for the player to deal with. If this were to be a full level or arcade mode in a game, we would likely make it so that the rate at which the hazards spawn becomes higher as the game goes on. We could also make the hazards more bouncy or sporadic in their movement to up the difficulty.

***


### **Aesthetics**


We aimed for the aesthetics of our prototype to use darker colours contrasted with harsh, bright colours to give off a sense of discomfort and stress. This is most evident with the halfpipe (a mix of bright red and black) and the hazards (which are also bright red). There is no in-between. There are intense darks and vibrant lights, with no neutral or subdued tones, which helps with the uneasy feeling we want our game to have. 

If we were to expand this into a full game, we would likely realize this aesthetic more, leaning further into the harsh contrasting colours. Perhaps we could even go the route of psychedelic visuals, though that could end up being too intense. Either way, we want our visuals to be relatively cartoonish, while still conveying the intensity and panic felt during gameplay. 

***



### **Sources**



###### **Coding Sources**



* Assignment 1 (*Applications of Game Engines*, GAME-10009): Player Controller, Camera Movement, & Collision with Hazards Using OnCollisionEnter2D



* [Freezing the Player-Character Upon Respawn Using Rigidbody2D Constraints](https://discussions.unity.com/t/how-do-i-freeze-a-rigidbody2d-using-script/196785/2)



* [Unfreezing the Player-Character Using Rigidbody2D WakeUp();](https://discussions.unity.com/t/rigidbody-constraints-not-unfreezing/801174/8)



###### **Asset Sources**



* [https://assetstore.unity.com/packages/2d/2d-kit-fantasy-game-stuff-248412#description](https://assetstore.unity.com/packages/2d/2d-kit-fantasy-game-stuff-248412#description)

***


### **Code Snippets**

###### **Collision with Hazard & Respawning with Physics Freeze**
```C#
 void Update()
 {
     if (Input.GetKeyDown(KeyCode.Space))
     {
         playerRigid.constraints = RigidbodyConstraints2D.None;
         playerRigid.WakeUp();
     }
 }

 private void OnCollisionEnter2D(Collision2D collision)
 {
     if (collision.gameObject.tag == "Player")
     {
         print("game over oat");

         playerBall.transform.position = new Vector2(0, 7);
         playerRigid.constraints = RigidbodyConstraints2D.FreezeAll;

     }
 }
```

###### **Player Controller**
```C#
 void Start()
 {
     rb2d = GetComponent<Rigidbody2D>();
     coyoteTime = coyoteTimeMax;
 }

 // Update is called once per frame
 void Update()
 {
     isGrounded = JumpRegister();

     if (isGrounded)
     {
         // print("yeah true");
         coyoteTime = coyoteTimeMax;
     }
     else
     {
         coyoteTime -= Time.deltaTime;
     }

     HandleMovement();
     HandleJump();
 }
 public void HandleMovement()
 {
     rb2d.velocity = new Vector2(rb2d.velocity.x + (Input.GetAxis("Horizontal") * movementInfluenceFactor),
     rb2d.velocity.y);

     if (rb2d.velocity.y > 0 && Input.GetKey(KeyCode.S)) // holding s?
     {
         rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y / 8);
     }
     else if (rb2d.velocity.y < 0 && Input.GetKey(KeyCode.S)) // holding s and starting to fall?
     {
         rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y - 0.025f);
     }

 }

 public void HandleJump()
 {
     if (coyoteTime > 0 && Input.GetKeyDown(KeyCode.Space))
     {
         // Jump using the Rigidbody velocity
         rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
     }

     if (rb2d.velocity.y < 0)
     {
         rb2d.velocity += (Vector2.up * Physics2D.gravity.y *
             (fallMultiplier * Time.deltaTime));
     }
     // ascending and space is not being held
     else if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
     {
         // if not pressing key, apply lowjump mult
         rb2d.velocity += (Vector2.up * Physics2D.gravity.y *
             (lowJumpMultiplier * Time.deltaTime));
     }
 }

 bool JumpRegister()
 {
     bool check = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, groundLayer); 
     // using a raycast to determine the players distance away from the ground using starting point, direction, size, and layer respectively
     return check;
 }
```

###### **Camera Movement**
```C#
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        float extraYPosition;
        float centerCalc = 1 / fromCenter;
        Vector3 cameraOffsetVar = cameraOffset;
        if (newPos.y > 0)
        {
            extraYPosition = newPos.y;
            fromCenter = 0.5f;
            cameraOffsetVar = new Vector3(cameraOffsetVar.x, -target.position.y / 1.4f, cameraOffsetVar.z);
        }
        else 
        extraYPosition = 0;

            transform.position = 
                Vector3.Lerp(transform.position, 
                new Vector3(newPos.x / centerCalc, extraYPosition + newPos.y / centerCalc, newPos.z) + cameraOffsetVar, 
                Time.deltaTime * smoothFactor);
    }
}
```

***



### **Prototype Iteration**



###### **Scrapped Ideas**



* For the initial concept of our prototype, we had the idea to have preplaced obstacles on the halfpipe itself and/or obstacles that would jut out of the halfpipe. We made the decision to change this to falling obstacles, as we thought that approach would be easier to implement and more manageable for the player.



* During the ideation phase, we considered making our prototype 3D. Players would roll down a 3D halfpipe, where they could see the hazards ahead of them as they rolled forward. We ultimately decided to do a 2D prototype, as we figured that could be easier, and also because we already did a 3D prototype previously.



* Our original plan for the hazard balls was to have them despawn upon contact with the halfpipe. We decided to not go forward with this, partly due to time constraints, but also because we found that having the hazards remain present added to the frantic nature of our prototype, and would force players to use their movement more strategically.



###### **Changed Ideas**



* We decided to give the player a buffer upon respawning, instead of having them simply respawn and drop down immediately. This prevents situations where players respawn directly on a hazard, or if a hazard immediately falls onto them. It essentially serves as our version of invincibility frames, giving the player a safe space before jumping back in. As a side note, if this were part of a full game, we would make sure this buffer only lasts a certain amount of time.



* In the beginning, we had the halfpipe be more open, but this caused issues where either the hazards (or the player) would get knocked out of the level, or the player could camp up at the top of the halfpipe. Thus, we made it so that the halfpipe is enclosed, and is more of a loop, preventing anything from escaping and ensuring players do not have anywhere to hide. Tweaking the player-character's physics also helped to keep the player from getting knocked off the level.



* We made it so that the camera follows the player around, making it easier for players to track where they are in relation to everything else, and also making movement feel more smooth and fluid.



* The player-character's physics were also changed to be much more floaty, to allow for ample freedom of movement, instead of being held down by gravity or mass and whatnot.



* As a final change, we altered the hazard balls to retain their movement a bit more, as well as act somewhat sporadically with increased bounciness. This makes the hazards more threatening, and they remain as a threat for much longer as they move around the halfpipe (as opposed to getting stuck at the bottom of the halfpipe fairly quickly).

