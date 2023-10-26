import React, { useState } from 'react';
import { FormControl, InputLabel, MenuItem, Select, Box, Grid } from '@material-ui/core';

import { useLanguage } from '../../contexts/LanguageContext'; // Importa el contexto

const languageOptions = [
  { value: 'es', label: 'Español', flag: "/images/flags/es_flag.png" }, // 'es' representa el código de idioma para español
  { value: 'en', label: 'English', flag: "/images/flags/en_flag.png" }, // 'en' representa el código de idioma para inglés
  { value: 'fr', label: 'Frances', flag: "/images/flags/fr_flag.png" }, // 'fr' representa el código de idioma para frances
  // Agrega más opciones de idioma según tus necesidades
];

const LanguageSelector = () => {
    const { selectedLanguage, setLanguage } = useLanguage(); // Obtiene el contexto

  const handleLanguageChange = (event) => {
    setLanguage(event.target.value);
    // Aquí puedes realizar cualquier acción que deba ejecutarse al cambiar el idioma, como cambiar la configuración de idioma en tu aplicación.
  };

  return (
    <FormControl>
      <InputLabel>Idioma</InputLabel>
      <Select
        value={selectedLanguage}
        onChange={handleLanguageChange}
        color='white'
      >
        {languageOptions.map((option) => (
          <MenuItem key={option.value} value={option.value}>
            <Grid container spacing={1} alignItems="center">
              <Grid item>
                <Box component="img" src={option.flag} alt={option.label} width={24} height={16} />
              </Grid>
              <Grid item>{option.label}</Grid>
            </Grid>
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
};

export default LanguageSelector;
