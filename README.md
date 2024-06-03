# Mini_Vampire_Survivors

This is the replica of the Vampire Survviour.

Info about the game.
there are total 3 enemy type (Normal , Fast , Big).
player will have one Weapon MagicWand.
After enemy die , xpGems will spawn.
XPGems will be used to LevelUp player.
every levelUp player will have choice of 3 powerUps (health, damge , Firerate)


Objetive:
if survive for 5 min  , won.
if player health reduce belove 0 then gameOver.


Programing Pattern Used:

Composition is used instead of Inheritance => so that long hyrachy is not needed and easily use components any other systems.

Observer Pattern => to decouple multiple system , and have a center point where all the data transfers.
EventManger class is the place where all events are take place witch is also singletone means anyone can subscribe any event and able to get inforamation when needed.

Finite State Machine Pattern => so that we can have full controll on each state of Player and Enemy. 
PlayerFSM , EnemyFSM are responsiable for handling their characters and their behaviour. ex: Idle , Survivng , Died   or Idle , Chase , Attack , Died
any new behaviour we can add or remove without effecting exiting behaviours.


Flyweight Pattern:
all the Sensitive data are stored in Scriptable object and passed to their respective system so that even if there are 100 enemies  but there will be only 1 config data in memeory.
plus it is very handy to edit and upadte value without getting into code or scene directlly form project asset.
we can currentlly edit ->
Player Config (max health , start level ,Move Speed , primary weapon)
XPLevel Config (all 10 levels data ,  list of powerUps , List of Gems will be spawn and collect after enemy die)
Spawn Config (Wave Config , witch enemy type when will be spawn)
Enemy Config (Each enemy will have thire cofig file attach , Health , AttackRange , Damage , MoveSpeed)


Singleton Pattern => So that we can have one object avaialbe globally to all.
States Manager -> responcible for all the states happing in game ex: surving Time , Total enemies killed count ,etc
withc is avaialbe glowbally to read but writable. will update value based on events when occure like when enemy died they will fire event and our stateManager will get notification and update total kill counter.

something called Mediater => this mediater will have refrence of all the systems high level controllers. so that multiple system can have access  to each other.
mediater => have refrence of gameManger , Player , XpLevelUpManager etc.
when enemy activates then they need a player transform to follow so they will acess through this mediater.


Game Flow =>
1.Gamemanager will fire event called OnGameStart with all the neccesary detaisl to start game.
every system will listen to this event and activate their systems like playerFSM , SpawnManager , StatesManaegr ,Uimanager.
2.Spawn manager will spawn enemies based on waveConfig and current Level.
3. Player will movearound and kill enemies and collect xpgems to levleup.
4. once player survive for 5 min or reduce health below 0 then  Gamemanager will fire event GameComplete and every system will listen to this and imidiatlly Deactivate their Systems.
5. game Over Ui will be shown
