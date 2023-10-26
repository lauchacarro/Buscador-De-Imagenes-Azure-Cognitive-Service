import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Rating from "@material-ui/lab/Rating";
import CircularProgress from "@material-ui/core/CircularProgress";
import axios from '../../axios.js';
import { useLanguage } from "../../contexts/LanguageContext";
import { useHistory } from "react-router-dom";

import "./Details.css";

export default function Details() {
  let { id } = useParams();
  const [document, setDocument] = useState({});
  const [selectedTab, setTab] = useState(0);
  const [isLoading, setIsLoading] = useState(true);
  const { selectedLanguage } = useLanguage();
  const history = useHistory();

  useEffect(() => {
    setIsLoading(true);

    axios
      .get("/api/images/" + id)
      .then((response) => {
        const doc = response.data;
        setDocument(doc);
        setIsLoading(false);
      })
      .catch((error) => {
        console.log(error);
        setIsLoading(false);
      });
  }, [id]);

  const deleteImageClickHandle = () => {
    axios
      .delete("/api/images/" + id)
      .then((_) => {
        setIsLoading(false);
        history.push("/search");
      })
      .catch((error) => {
        console.log(error);
        setIsLoading(false);
      });
  };

  // View default is loading with no active tab
  let detailsBody = <CircularProgress />,
    resultStyle = "nav-link",
    rawStyle = "nav-link";

  if (!isLoading && document) {
    // View result
    if (selectedTab === 0) {
      resultStyle += " active";
      detailsBody = (
        <div className="card-body">
          <h5 className="card-title">
            {document["nombre_" + selectedLanguage]}
          </h5>
          <img className="image" src={document.url} alt="Book cover"></img>
          <p className="card-text">
            <strong>Descripción</strong>
          </p>
          <p className="card-text">
            {document["descripcion_" + selectedLanguage]}
          </p>
          <p className="card-text">
            <strong>Etiquetas</strong>
          </p>
          <p className="card-text">
            {document["etiquetas_" + selectedLanguage]?.join("; ")}
          </p>
          <p className="card-text">
            <strong>Objetos</strong>
          </p>
          <p className="card-text">
            {document["objetos_" + selectedLanguage]?.join("; ")}
          </p>
          <p className="card-text">
            <strong>Palabras</strong>
          </p>
          <p className="card-text">
            {document["palabras_" + selectedLanguage]?.join("; ")}
          </p>
          <p className="card-text">
            <strong>Leyendas</strong>
          </p>
          <p className="card-text">
            {document["leyendas_" + selectedLanguage]?.join("; ")}
          </p>
          <p className="card-text">
            <strong>Sinonimo</strong>
          </p>
          <p className="card-text">
            {document["sinonimos_" + selectedLanguage]?.join("; ")}
          </p>
        </div>
      );
    }

    // View raw data
    else {
      rawStyle += " active";
      detailsBody = (
        <div className="card-body text-left">
          <pre>
            <code>{JSON.stringify(document, null, 2)}</code>
          </pre>
        </div>
      );
    }
  }

  return (
    <main className="main main--details container fluid">
      <div className="card text-center result-container">
        <div className="card-header">
          <ul className="nav nav-tabs card-header-tabs">
            <li className="nav-item">
              <button className={resultStyle} onClick={() => setTab(0)}>
                Result
              </button>
            </li>
            <li className="nav-item">
              <button className={rawStyle} onClick={() => setTab(1)}>
                Raw Data
              </button>
            </li>
          </ul>
        </div>
        {detailsBody}
        <button className="btn btn-danger" onClick={deleteImageClickHandle}>
          Eliminar
        </button>
      </div>
    </main>
  );
}
