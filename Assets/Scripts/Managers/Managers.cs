using UnityEditor;
using UnityEngine;

public class Managers : MonoBehaviour
{
	static Managers _instance;
	static Managers Instance { get { Init(); return _instance; } }

    CoroutineManager _coroutine = new CoroutineManager();
    DataManager _data = new DataManager();
    GameManagerEx _game = new GameManagerEx();
    InputManager _input = new InputManager();
	ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
	UIManager _ui = new UIManager();

	public static CoroutineManager Coroutine { get { return Instance._coroutine; } }
    public static DataManager Data { get { return Instance._data; } }
	public static GameManagerEx Game { get { return Instance._game; } }
    public static InputManager Input { get { return Instance._input; } }
	public static ResourceManager Resource { get { return Instance._resource; } }
	public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
	public static UIManager UI { get { return Instance._ui; } }

	void Start()
	{
		Init();
		Resources.UnloadUnusedAssets();
				
	}	

	static void Init()
	{
		Application.targetFrameRate = 60;
		if(_instance == null)
		{
			GameObject manager = GameObject.Find("@Managers");
			if(manager == null)
			{
				manager = new GameObject { name = "@Managers" };
				manager.AddComponent<Managers>();
			}
			DontDestroyOnLoad(manager);
			_instance = manager.GetComponent<Managers>();

			Data.Init();
            Sound.Init();
        }
	}

	private void Update()
	{
		_input.OnUpdate();
    }

	public static void Clear()
	{
		//입력
		Input.Clear();
		//사운드
		Sound.Clear();
		//씬
		//ui
		//UI.Clear();
		Coroutine.Clear();
		
	}
}
