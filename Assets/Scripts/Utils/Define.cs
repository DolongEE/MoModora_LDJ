public class Define
{
	public enum WorldObject
	{
		Unknown,
		Player,
		//TODO: 몬스터 종류 추가
	}

	public enum State
	{
		None,
		Die,
		Idle,
		Moving,
		Attack,
		Jump,
		Roll,
	}

    public enum PlayerStates
    {
		None,
        Death,
        Idle,
        Move,
        Jump,
		Crouch,
        Attack,
		Bow,
        Roll,
    }

    public enum Sound
	{
		Bgm,
		Effect,
		UI,
		MaxCount,
	}

	public enum Scene
	{
		Unknown,
		Lobby,
		Game,
	}

	public enum KeyActionUI
	{
		None,
		Select,
		Back,
    }

}
