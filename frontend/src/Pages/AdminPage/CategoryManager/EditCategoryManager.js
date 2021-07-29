import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { Form, Input, Button } from "antd";
import { authHeader } from "../../../Service/AuthService";
import axios from "axios";
const EditCategoryManager = () => {
  const [category, setCategory] = useState({});

  const { categoryId } = useParams();

  useEffect(() => {
    axios({
      method: "get",
      url: `https://localhost:5001/api/category/${categoryId}`,
      headers: authHeader(),
    })
      .then((response) => {
        setCategory(response.data);

        console.log("book", response);
      })
      .catch((error) => {
        // handle error
        console.log(error);
      });
  }, [categoryId]);

  const onFinish = (values) => {
    console.log("Success:", values.name);
    axios({
      method: "put",
      url: `https://localhost:5001/api/category`,
      headers: authHeader(),
      data: {
        id: category.id,
        name: values.name,
        books: null,
      },
    })
      .then((res) => {
        console.log(res.data);
      })
      .catch((err) => console.log(err));
  };

  const onFinishFailed = (errorInfo) => {
    console.log("Failed:", errorInfo);
  };
  console.log("category", category);
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
export default EditCategoryManager;
