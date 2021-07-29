import axios from "axios";
import { useEffect, useState } from "react";

import { authHeader } from "../../../Service/AuthService";
import { Table, Space, Button } from "antd";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
const ListBookManager = () => {
  const [books, setBooks] = useState([]);
 
  const [changes, setChanges] = useState(false);

  const deleteBook = (bookId) => {
    axios({
      method: "delete",
      url: `https://localhost:5001/api/book/${bookId}`,
      headers: authHeader(),
    })
      .then(() => {
        setChanges(!changes);
      })
      .catch((err) => console.log(err));
  };
  const handleDelete = (id) => {
    if (window.confirm("Are you sure to delete this book?")) {
      deleteBook(id);
    }
    //console.log("id",id)
  };

  useEffect(() => {
    axios({
      method: "get",
      url: "https://localhost:5001/api/books",
      headers: authHeader(),
    })
      .then((response) => {
        setBooks(response.data);

      })
      .catch((error) => {
        // handle error
        console.log(error);
      });
  }, [changes]);
  const columns = [
    {
      title: "id",
      dataIndex: "id",
      key: "id",
    },
    {
      title: "Book",
      dataIndex: "name",
      key: "Book",
    },
    {
      title: "Author",
      dataIndex: "author",
      key: "Author",
    },
    {
      title: "Categoryid",
      dataIndex: "categoryId",
      key: "Categoryid",
    },
    {
      title: "Action",
      dataIndex: "Action",
      key: "Action",
      render: (text, record) => (
        <Space size="middle">
          <Button type="primary">
            <Link to={`/admin/books/${record.id}/edit`}>Edit</Link>
          </Button>
          <Button type="primary" danger onClick={() => handleDelete(record.id)}>
            Delete
          </Button>
        </Space>
      ),
    },
  ];

  return (
    <div>
      <Button
        type="primary"
        style={{
          marginBottom: 16,
          marginLeft:16
        }}
      >
        <Link to="/admin/addBook">Add</Link>
      </Button>
      <Table columns={columns} dataSource={books} />
    </div>
  );
};
export default ListBookManager;
