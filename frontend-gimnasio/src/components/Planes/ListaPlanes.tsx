import { useState, useEffect } from 'react';
import axios from 'axios';
import { Plan } from './Plan';

interface PlanData {
    idPlan: number;
    nombre: string;
    precio: number;
    descripcion: string;
}

export function ListaPlanes() {
    const [planes, setPlanes] = useState<PlanData[]>([]);

    useEffect(() => {
       
        axios.get('http://localhost:5024/api/planes')
            .then(respuesta => {
                setPlanes(respuesta.data);
            })
            .catch(error => {
                console.error("Error al cargar los planes:", error);
            });
    }, []);

    return (
        <ul className="list-group">
            {planes.map(plan => (
                <Plan 
                    key={plan.idPlan} 
                    nombre={plan.nombre} 
                    precio={plan.precio} 
                    descripcion={plan.descripcion} 
                />
            ))}
        </ul>
    );
}