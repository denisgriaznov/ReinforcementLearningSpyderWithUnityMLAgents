from mlagents_envs.environment import UnityEnvironment
from mlagents_envs.envs.unity_gym_env import UnityToGymWrapper
from stable_baselines3 import SAC
from stable_baselines3.common.logger import configure


def init_env():
    unity_env = UnityEnvironment("AI Spyder/spyder_walker/AI Spyder.exe")
    env = UnityToGymWrapper(unity_env, uint8_visual=True)
    print("Observations shape: ", env.observation_space.shape)
    print("Actions shape: ", env.action_space.shape)
    return env


def learn_sac(timesteps=200000, model_path="sac_model.zip"):
    env = init_env()
    tmp_path = "/progress/"
    logger = configure(tmp_path, ["csv"])
    model = SAC("MlpPolicy", env, verbose=1, learning_rate=0.001)
    model.set_logger(logger)
    model.learn(total_timesteps=timesteps, log_interval=4)
    model.save(model_path)
    env.close()


learn_sac(model_path="models/sac_model.zip")