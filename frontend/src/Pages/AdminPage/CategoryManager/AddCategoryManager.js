import { Form, Input, Button } from "antd";
import { authHeader } from "../../../Service/AuthService";
import axios from "axios";
const AddCategoryManager = () => {
  const onFinish = (values) => {
    console.log("Success:", values.name);
    axios({
      method: "post",
      url: `https://localhost:5001/api/category`,
      headers: authHeader(),
      data: {
        name: values.name,
        books: null,
      },
    })
      .then((res) => {
        console.log("Success", res.data);
      })
      .catch((err) => console.log(err));
  };

  const onFinishFailed = (errorInfo) => {
    console.log("Failed:", errorInfo);
  };

  return (
    <Form
      name="basic"
      labelCol={{
        span: 8,
      }}
      wrapperCol={{
        span: 8,
      }}
      initialValues={{
        remember: true,
      }}
      onFinish={onFinish}
      onFinishFailed={onFinishFailed}
    >
      <Form.Item
        label="Category name"
        name="name"
        rules={[
          {
            required: true,
            message: "Please input ",
          },
        ]}
      >
        <Input />
      </Form.Item>

      <Form.Item
        wrapperCol={{
          offset: 8,
          span: 16,
        }}
      >
        <Button type="primary" htmlType="submit">
          Submit
        </Button>
      </Form.Item>
    </Form>
  );
};
export default AddCategoryManager;
