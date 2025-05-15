const API = "https://localhost:5001/api"; 


//get all stylists
export const getStylists = async() => {
    const res = await fetch (`${API}/stylists`);
    return res.json ();
}


//create stylist
export const createStylist = async (newStylist) => {
    const res = await fetch(`${API}/stylists`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(newStylist),
    });
    return res.json();
}