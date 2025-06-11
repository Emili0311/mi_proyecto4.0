const Presentacion = () => {
    const date = new Date();

    return (
        <div>{date.toISOString()}</div>
    )
}

export default Presentacion;