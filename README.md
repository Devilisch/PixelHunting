# PixelHunting
Test project

**Unity version:** 2020.3.32f1
**Platform:** Android
**Screen:** Vertical
**Review:** (RU) https://drive.google.com/file/d/1T0AzqOYW1SJinKi9aplllywANkHLLqkj/view?usp=sharing


**TO DO**:
+ *Architecture:*
- :white_check_mark: Separate logic and view
+ *Unity:*
- :white_check_mark: Animation (controller, logic of transitions between states)
- :white_check_mark: Raycasting
- :white_check_mark: Collider
- :white_check_mark: Coroutine
- :white_check_mark: UI scrolling user interface
- :white_check_mark: UI mask
- :white_check_mark: Materials: creation and modification in runtime
+ *C#:*
- :white_check_mark: Singleton
- :white_check_mark: Enum
- :white_check_mark: Switch
- :white_check_mark: File management
- :white_check_mark: Threads
- :white_check_mark: JSON
- :white_check_mark: Object pool
- :white_check_mark: Actions
+ *OOP:*
- :white_check_mark: Inheritance (create a class based on the base one)
- :white_check_mark: Interface


**Game components:**
- Player controls hero by tapping on the screen
- There are 2 types of objects on the map - enemies and fruits
- Touching the fruit the player gets points
- Touching an enemy the player loses lives
- When the player runs out of lives, the game ends
- Enemies move chaotically across the map
- Fruits are motionless


**GUI:**
- Start menu with the Play button
- In-game interface displaying lives and points
- Game over window with score


**Code requirements:**
- Do not use ECS
- Minimize the use of MonoBehavior functions
- Avoid public functions
- Do not use Zenject and its analogues