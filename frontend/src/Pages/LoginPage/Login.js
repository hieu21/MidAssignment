import { Form, Input, Button, Checkbox } from "antd";
import axios from 'axios'
import { useContext, useState } from "react";
import "antd/dist/antd.css";
import UserContext from "../../Context/UserContext";

const Login = () => {

  const { currentUser, setCurrentUser } = useContext(UserContext);
  const onFinish = (values) => {
    console.log("Success:", values);
    axios({
      method: "Post",
      url: "https://localhost:5001/User/Login",
      data: JSON.stringify(values),
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    })
      .then((res) => {
        if (res.data.id) {
          localStorage.setItem("token", JSON.stringify(res.data));
          setCurrentUser(res.data);
          console.log(res.data.id)
        }
      })
      .catch((error) => console.log(error));
  };

  const onFinishFailed = (errorInfo) => {
    console.log("Failed:", errorInfo);
  };

  return (
    <div>
        <h1 style={{textAlign:'center'}}>Login</h1>
        <Form
          name="basic"
          labelCol={{
            span: 10,
          }}
          wrapperCol={{
            span: 6,
          }}
          initialValues={{
            remember: true,
          }}
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
        >

          <Form.Item
            label="Username"
            name="username"
            rules={[
              {
                required: true,
                message: "Please input your username!",
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="Password"
            name="password"
            rules={[
              {
                required: true,
                message: "Please input your password!",
              },
            ]}
          >
            <Input.Password />
          </Form.Item>

          <Form.Item
            name="remember"
            valuePropName="checked"
            wrapperCol={{
              offset: 10,
              span: 16,
            }}
          >
            <Checkbox>Remember me</Checkbox>
          </Form.Item>

          <Form.Item
            wrapperCol={{
              offset: 10,
              span: 16,
            }}
          >
            <Button type="primary" htmlType="submit">
              Submit
            </Button>
          </Form.Item>
        </Form>
    </div>
  );
};

export default Login;
