const API = "https://localhost:5001/api"; 


//get all customers
export const getCustomers = async() => {
    const res = await fetch (`${API}/customers`);
    return res.json ();
}