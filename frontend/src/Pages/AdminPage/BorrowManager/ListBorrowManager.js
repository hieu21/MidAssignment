import axios from "axios";
import { useEffect, useState } from "react";

import { authHeader } from "../../../Service/AuthService";
import { Table, Space, Button } from "antd";
const ListBorrowManager = () => {
  const [borrow, setBorrow] = useState([]);
  const [loading, setloading] = useState(true);

  const handleApprove = (id) => {
    if (window.confirm("Are you sure to approve this book?")) {
      axios({
        method: "put",
        url: `https://localhost:5001/BookBorrowingRequest/${id}/approve`,
        headers: authHeader(),
      })
        .then((res) => {
          console.log(res.data);
          setloading(!loading);
        })
        .catch((err) => console.log(err));
    }
  };
  const handleReject = (id) => {
    if (window.confirm("Are you sure to reject this book?")) {
      axios({
        method: "put",
        url: `https://localhost:5001/BookBorrowingRequest/${id}/reject`,
        headers: authHeader(),
      })
        .then((res) => {
          console.log(res.data);
          setloading(!loading);
        })
        .catch((err) => console.log(err));
    }
  };

  useEffect(() => {
    axios({
      method: "get",
      url: "https://localhost:5001/api/BorrowRequests",
      headers: authHeader(),
    })
      .then((response) => {
        setBorrow(response.data);
      })
      .catch((error) => {
        // handle error
        console.log(error);
      });
  }, [loading]);
  const columns = [
    {
      title: "id",
      dataIndex: "id",
      key: "id",
    },
    {
      title: "UserId",
      dataIndex: "userId",
      key: "userId",
    },
    {
      title: "BorrowDate",
      dataIndex: "borrowDate",
      key: "borrowDate",
    },
    {
      title: "Status",
      dataIndex: "status",
      key: "status",
    },
    {
      title: "Action",
      dataIndex: "Action",
      key: "Action",
      render: (text, record) => (
        <Space size="middle">
          <Button type="primary" onClick={() => handleApprove(record.id)}>
            Approve
          </Button>
          <Button type="primary" danger onClick={() => handleReject(record.id)}>
            Reject
          </Button>
        </Space>
      ),
    },
  ];

  return (
    <div>
      <Table columns={columns} dataSource={borrow} />
    </div>
  );
};
export default ListBorrowManager;
