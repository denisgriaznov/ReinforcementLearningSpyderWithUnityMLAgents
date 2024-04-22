from mlagents_envs.environment import UnityEnvironment
from mlagents_envs.envs.unity_gym_env import UnityToGymWrapper
import gym
from stable_baselines3 import SAC
import torch
import onnxruntime as ort
import numpy as np


def init_env(env_path="spyder_walker/AI Spyder.exe"):
    unity_env = UnityEnvironment(env_path)
    env = UnityToGymWrapper(unity_env, uint8_visual=True)
    print("Observations shape: ", env.observation_space.shape)
    print("Actions shape: ", env.action_space.shape)
    return env


def test_sac(episodes=4, env_path="spyder_walker/AI Spyder.exe", model_path="sac_model.zip"):
    env = init_env(env_path)
    model = SAC.load(model_path)
    for _ in range(episodes):

        obs = env.reset()
        terminated = False
        while not terminated:
            action, _states = model.predict(obs, deterministic=True)
            obs, reward, terminated, info = env.step(action)

    env.close()
