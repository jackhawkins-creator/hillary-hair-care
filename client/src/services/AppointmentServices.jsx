const API = "https://localhost:5001/api"; 


//get all appointments
export const getAppointments = async() => {
    const res = await fetch (`${API}/appointments`);
    return res.json ();
}


// delete appointments
export const deleteAppointment = async (id) => {
    const res = await fetch(`${API}/appointments/${id}`, {
        method: "DELETE",
    });
    return res.json ();
}

//create appointments
export const createAppointment = async (newAppointment) => {
    const res = await fetch(`${API}/appointments`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(newAppointment),
    });
    return res.json();
}