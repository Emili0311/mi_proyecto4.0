import { useEffect, useState } from "react";

type Equipo = {
    "Id": string,
    "Escuela": string,
    "Carrera": string,
    "Grupo": string,
    "DatosSemestre": string,
    "ProyectoNombre": string,
    "Integrante1": string,
    "Integrante2": string,
    "Fecha": string
}

const Presentacion = () => {
    const [equipo, setEquipo] = useState<Equipo>();

    //Manejo de Datos
    const cargarDatos = async () => {
        const resp = await fetch("/mi-proyecto/presentacion");
        if(resp.ok) {
            const datos = await resp.json();
            console.log(datos);
            setEquipo(datos);
        }
    }
    useEffect(()=>{
        cargarDatos();
    },[]);

    const formatoFecha = (fecha: string | undefined) => {
        if(fecha){
            const formato = new Intl.DateTimeFormat("es-Mx", {year: "numeric", month: "2-digit", day: "2-digit"});
            const dt = new Date(fecha);

            return formato.format(dt);
        }
        return undefined;
    }
    //Vista
    return(
        <>
            <div className="display-4 text-center">{equipo?.Escuela}</div>
            <div className="h1 text-center mt-4">{equipo?.Carrera}</div>
            <div className="h1 text-center mt-4">{equipo?.DatosSemestre}</div>
            <div className="h1 text-center">{equipo?.Grupo}</div>
            <div className="h1 text-center mt-4">Integrantes</div>
            <div className="h2 text-center text-primary">{equipo?.Integrante1}</div>
            <div className="h2 text-center text-primary">{equipo?.Integrante2}</div>
            <div className="h1 text-center mt-4">Nombre del Proyecto</div>
            <div className="h2 text-center">{equipo?.ProyectoNombre}</div>
            <div className="h4 text-center mt-4">{formatoFecha(equipo?.Fecha)}</div>
        </>
    )
}

export default Presentacion;