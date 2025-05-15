const API = "https://localhost:5001/api"; 


//get all customers
export const getCustomers = async() => {
    const res = await fetch (`${API}/customers`);
    return res.json ();
}

//create customer
export const createCustomer = async (newCustomer) => {
    const res = await fetch(`${API}/customers`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(newCustomer),
    });
    return res.json();
}