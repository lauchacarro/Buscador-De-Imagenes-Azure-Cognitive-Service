import React from "react";
import LanguageSelector from "../LanguageSelector/LanguageSelector";
import "./AppHeader.css";

export default function AppHeader() {
  return (
    <header className="header">
      <nav className="navbar navbar-expand-lg">
        <a className="navbar-brand" href="/">
          <img
            src="/images/mouron-it-logo.png"
            className="navbar-logo"
            alt="Mouron It"
          />
        </a>
        <button
          className="navbar-toggler"
          type="button"
          data-toggle="collapse"
          data-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item">
              <a className="nav-link" href="/Search">
                Buscar
              </a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/Upload">
                Subir Imagen
              </a>
            </li>
          </ul>
        </div>

        <LanguageSelector />
      </nav>
    </header>
  );
}
