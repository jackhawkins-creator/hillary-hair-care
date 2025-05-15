const API = "https://localhost:5001/api"; 


//get all services
export const getServices = async() => {
    const res = await fetch (`${API}/services`);
    return res.json ();
}