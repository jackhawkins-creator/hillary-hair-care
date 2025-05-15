const API = "https://localhost:5001/api"; 


//get all stylists
export const getStylists = async() => {
    const res = await fetch (`${API}/stylists`);
    return res.json ();
}