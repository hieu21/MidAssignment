import axios from "axios";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { authHeader } from "../../../Service/AuthService";
import { Table, Space, Button } from "antd";
const ListCategoryManager = () => {
  const [categories, setCategories] = useState([]);

  const [changes, setChanges] = useState(false);
  const deleteCategory = (categoryId) => {
    axios({
      method: "delete",
      url: `https://localhost:5001/api/category/${categoryId}`,
      headers: authHeader(),
    })
      .then(() => {
        setChanges(!changes);
      })
      .catch((err) => console.log(err));
  };
  const handleDelete = (id) => {
    if (window.confirm("Are you sure to delete this book?")) {
      deleteCategory(id);
    }
  };

  useEffect(() => {
    axios({
      method: "get",
      url: "https://localhost:5001/api/categories",
      headers: authHeader(),
    })
      .then((response) => {
        setCategories(response.data);
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
      title: "Category",
      dataIndex: "name",
      key: "Category",
    },
    {
      title: "Action",
      dataIndex: "Action",
      key: "Action",
      render: (text, record) => (
        <Space size="middle">
          <Button type="primary">
            <Link to={`/admin/categories/${record.id}/edit`}>Edit</Link>
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
          marginLeft: 16,
        }}
      >
        <Link to="/admin/addCategory">Add</Link>
      </Button>
      <Table columns={columns} dataSource={categories} />
    </div>
  );
};
export default ListCategoryManager;
