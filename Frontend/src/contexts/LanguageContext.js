import React, { createContext, useContext, useState } from 'react';

const LanguageContext = createContext();

export function useLanguage() {
  return useContext(LanguageContext);
}

export function LanguageProvider({ children }) {
  const [selectedLanguage, setSelectedLanguage] = useState('es'); // Establece el idioma predeterminado

  const setLanguage = (language) => {
    setSelectedLanguage(language);
  };

  return (
    <LanguageContext.Provider value={{ selectedLanguage, setLanguage }}>
      {children}
    </LanguageContext.Provider>
  );
}
