from learn import *


# test model on 4 episodes
def test_sac(episodes=4, env_path="AI Spyder/spyder_walker/AI Spyder.exe", model_path="models/sac_model.zip"):
    env = init_env(env_path)
    model = SAC.load(model_path)
    for _ in range(episodes):
        obs = env.reset()
        terminated = False
        while not terminated:
            action, _states = model.predict(obs, deterministic=True)
            obs, reward, terminated, info = env.step(action)
    env.close()


test_sac(episodes=4, env_path="AI Spyder/spyder_walker/AI Spyder.exe", model_path="models/sac_model.zip")
