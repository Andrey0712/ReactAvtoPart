import { useSelector } from 'react-redux';


const CartDialog = () => {


    const { list } = useSelector(state => state.cart);
    const {username} = useSelector(res=>res.auth);



    return (
        <div className="row">
        <div className="offset-md-3 col-md-6">
            <h1 className="text-center"> Карзина {username}</h1>
            {<table className="table">
                <thead className="table table-bordered">
                    <tr>
                        {/* <th scope="col">Id</th> */}
                        <th scope="col">Image</th>
                        <th scope="col">Name</th>
                        <th scope="col">Price</th>
                        <th scope="col">Quantity</th>
                    </tr>
                </thead>
                <tbody>
                    {list && list.map((item) =>
                        <tr key={item.id}>
                            {/* <td>{item.id}</td> */}
                            <td>
                                <img src={item.productImage}
                                    alt=" "
                                    width="100"
                                />
                            </td>
                            <td>{item.productName}</td>
                            <td> {item.productPrice} </td>
                            <td> {item.quantity} </td>

                        </tr>)}
                </tbody>
            </table>}
        </div>
        </div>
    );
}
export default CartDialog;