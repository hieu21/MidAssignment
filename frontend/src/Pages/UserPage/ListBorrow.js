import { authHeader } from "../../Service/AuthService";
import CartContext from "../../Context/CartContext";
import UserContext from "../../Context/UserContext";
import { useContext, useEffect } from "react";
import { Table, Space, Button } from "antd";
import axios from "axios";
const ListBorrow = () => {
  const { currentUser } = useContext(UserContext);
  const { cart, setCart } = useContext(CartContext);
  const removerBookFromCart = (bookId) => {
    if (cart) {
      const index = cart.findIndex((item) => item.id === bookId); //tim vi tri can xoa
      cart.splice(index, 1); //xoa di 1 don vi tai index
      setCart(cart);
    }
  };
  const handleBorrowBook = () => {
    const books = {
      borrowRequestDetails: [],
    };
    if (cart) {
      for (let item of cart) {
        books.borrowRequestDetails.push({ bookId: item.id });
      }

      axios({
        method: "post",
        url: `https://localhost:5001/BookBorrowingRequest/${currentUser.id}`,
        headers: authHeader(),
        data: books,
      })
        .then((res) => {
          console.log(res.data);
        })
        .catch((err) => {
          if (err.response.status === 400) {
            alert(
              "Bạn chỉ được mượn tối đa 3 lần trong một tháng và mỗi lần chỉ được mượn 5 cuốn sách"
            );
          }
        });
    }
  };

  useEffect(() => {}, [cart]);
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
          <Button
            type="primary"
            danger
            onClick={() => removerBookFromCart(record.id)}
          >
            Delete
          </Button>
        </Space>
      ),
    },
  ];
  return (
    <div>
      <Table columns={columns} dataSource={cart} />
      <Button type="primary" onClick={handleBorrowBook}>
        Borrow
      </Button>
    </div>
  );
};
export default ListBorrow;
