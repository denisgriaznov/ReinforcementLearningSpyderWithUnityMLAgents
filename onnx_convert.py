from typing import Tuple, Any
import torch as th
from torch.nn import Parameter
from stable_baselines3 import SAC
import onnxruntime as ort
import numpy as np

CONTINUOUS_ACTIONS_SIZE = 12


class OnnxablePolicy(th.nn.Module):
    def __init__(self, actor: th.nn.Module):
        super().__init__()
        self.actor = actor
        self.version_number = Parameter(th.Tensor([3]), requires_grad=False)
        self.memory_size = Parameter(th.Tensor([0]), requires_grad=False)
        self.continuous_action_output_shape = Parameter(th.Tensor([CONTINUOUS_ACTIONS_SIZE]), requires_grad=False)

    def forward(self, observation: th.Tensor) -> tuple[Any, Parameter, Parameter, Parameter]:
        return self.actor(observation,
                          deterministic=True), self.continuous_action_output_shape, self.version_number, self.memory_size


def convert_to_onnx(model_path, output_path):
    model = SAC.load(model_path, device="cpu")
    onnxable_model = OnnxablePolicy(model.policy.actor)

    observation_size = model.observation_space.shape
    dummy_input = th.randn(1, *observation_size)
    th.onnx.export(
        onnxable_model,
        dummy_input,
        output_path,
        opset_version=17,
        input_names=["obs_0"],
        output_names=["continuous_actions", "continuous_action_output_shape", "version_number", "memory_size"],
    )

    observation = np.zeros((1, *observation_size)).astype(np.float32)
    ort_sess = ort.InferenceSession(output_path)
    scaled_action = ort_sess.run(None, {"obs_0": observation})[0]

    print(scaled_action)

    with th.no_grad():
        print(model.actor(th.as_tensor(observation), deterministic=True))


convert_to_onnx(model_path="models/sac_model_gamma_95.zip", output_path="models/sac_model_gamma_95.onnx")
