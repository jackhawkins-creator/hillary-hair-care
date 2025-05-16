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

//edit stylist
export const editStylist = async (id, updatedStylist) => {
    const res = await fetch(`${API}/stylists/${id}`, {
        method: "PATCH",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(updatedStylist),
    });

    if (!res.ok) {
        throw new Error("Failed to update stylist");
    }

    // Only return JSON if there is content
    if (res.status !== 204) {
        return res.json();
    }
};
