import React, { useState, useEffect, useRef } from "react";
import CircularProgress from "@material-ui/core/CircularProgress";
import axios from '../../axios.js';
import { Clear, CloudUploadOutlined } from "@material-ui/icons";
import { useHistory } from "react-router-dom";

import "./Upload.css";

export default function Upload() {
  const history = useHistory();

  const [file, setFile] = useState();
  const [fileUrl, setFileUrl] = useState();
  const [loading, setLoading] = useState(false);
  const hiddenFileInput = useRef(null);

  const handleClick = (event) => {
    hiddenFileInput.current.click();
  };
  // Call a function (passed as a prop from the parent component)
  // to handle the user-selected file
  const handleChange = (event) => {
    if (event.target.files?.length > 0) {
      const newFiles = Array.from(event.target.files);
      setFile(newFiles[0]);
      setFileUrl(URL.createObjectURL(newFiles[0]));
    }
  };

  const handleDrop = (event) => {
    event.preventDefault();
    const droppedFiles = event.dataTransfer.files;
    if (droppedFiles.length > 0) {
      const newFiles = Array.from(droppedFiles);
      setFile(newFiles[0]);
      setFileUrl(URL.createObjectURL(newFiles[0]));
    }
  };
  const handleRemoveFile = (event) => {
    event.preventDefault();
    setFile(null);
    setFileUrl(null);
  };

  const handleUploadButtonClick = () => {
    setLoading(true);
    if (file) {
      const formData = new FormData();
      formData.append("file", file);

      axios
        .post("/api/images/upload", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        })
        .then((response) => {
          setLoading(false);
          history.push("/details/" + response.data.id);
        })
        .catch(() => {
          setLoading(false);
        });
    }
  };

  return (
    <main className="main main--details container fluid ">
      <div className="card text-center result-container">
        {loading && <CircularProgress />}
        {!loading && (
          <section className="drag-drop" style={{ width: 500, height: 250 }}>
            <div
              className={`document-uploader ${
                file ? "upload-box active" : "upload-box"
              }`}
              onDrop={handleDrop}
              onDragOver={(event) => event.preventDefault()}
            >
              <>
                <input
                  type="file"
                  onChange={handleChange}
                  ref={hiddenFileInput}
                  style={{ display: "none" }}
                />
                <div className="upload-info" onClick={handleClick}>
                  <CloudUploadOutlined />
                  <div>
                    <p>Drag and drop your files here</p>
                    <p>Supported files: .JPG, .PNG</p>
                  </div>
                </div>
                {file && (
                  <div className="file-list">
                    <div className="file-list__container">
                      <div className="file-item">
                        <div className="file-info">
                          <p>{file.name}</p>
                          {/* <p>{file.type}</p> */}
                        </div>
                        <div className="file-actions">
                          <Clear onClick={handleRemoveFile} />
                        </div>
                      </div>
                      <div className="file-item justify-content-center">
                        <img width={150} src={fileUrl} />
                      </div>

                      <div className="file-item justify-content-center">
                        <button
                          className="btn btn-primary"
                          onClick={handleUploadButtonClick}
                        >
                          Subir Imagen
                        </button>
                      </div>
                    </div>
                  </div>
                )}
              </>
            </div>
          </section>
        )}
      </div>
    </main>
  );
}
