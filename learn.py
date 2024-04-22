from mlagents_envs.environment import UnityEnvironment
from mlagents_envs.envs.unity_gym_env import UnityToGymWrapper
import gym
from stable_baselines3 import SAC
import torch
import onnxruntime as ort
import numpy as np


def init_env():
    unity_env = UnityEnvironment("spyder_walker/AI Spyder.exe")
    env = UnityToGymWrapper(unity_env, uint8_visual=True)
    print("Observations shape: ", env.observation_space.shape)
    print("Actions shape: ", env.action_space.shape)
    return env


def learn_sac(timesteps=100000, model_path="sac_model.zip"):
    env = init_env()
    model = SAC("MlpPolicy", env, verbose=1, learning_rate=0.001, tau=0.03, tensorboard_log="./sac_spyder_tensorboard/")
    model.learn(total_timesteps=timesteps, log_interval=4)
    model.save(model_path)
    env.close()


