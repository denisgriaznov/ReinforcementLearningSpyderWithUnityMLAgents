from learn import *
from test import *
from onnx_convert import *

if __name__ == "__main__":
    learn_sac(model_path="sac_model.zip")
    # test_sac(episodes=4, env_path="spyder_walker/AI Spyder.exe", model_path="sac_model.zip")
    convert_to_onnx(model_path="sac_model.zip", output_path="sac_model.onnx")
