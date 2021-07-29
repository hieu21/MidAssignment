import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { Form, Input, Button,  } from "antd";
import { authHeader } from "../../../Service/AuthService";
import axios from "axios";
const EditBookManager = () => {
  const [books, setBooks] = useState({});
  
  const { bookId } = useParams();

  useEffect(() => {
    axios({
      method: "get",
      url: `https://localhost:5001/api/book/${bookId}`,
      headers: authHeader(),
    })
      .then((response) => {
        setBooks(response.data);
        
        console.log("book", response);
        
      })
      .catch((error) => {
        // handle error
        console.log(error);
      });
  }, [bookId]);

  const onFinish = (values) => {
    console.log("Success:", values.name);
    axios({
      method: "put",
      url: `https://localhost:5001/api/book`,
      headers: authHeader(),
      data: {
        id: books.id,
        name: values.name,
        author: values.author,
        categoryId: values.categoryid,
        category:null,
        borrowRequestDetails:null
      },
    })
      .then((res) => {
          console.log(res.data)
      })
      .catch((err) => console.log(err));
  };

  const onFinishFailed = (errorInfo) => {
    console.log("Failed:", errorInfo);
  };
  console.log("book", books);
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
        label="name"
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
        label="author"
        name="author"
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
        label="categoryid"
        name="categoryid"
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
export default EditBookManager;
