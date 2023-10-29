import React from "react";
import { List, Chip } from "@material-ui/core";
import CheckboxFacet from "./CheckboxFacet/CheckboxFacet";
import styled from "styled-components";
import { useLanguage } from "../../contexts/LanguageContext";

import "./Facets.css";

export default function Facets(props) {
  const { selectedLanguage } = useLanguage();

  function mapFacetName(facetName) {
    const capitalizeFirstLetter = (string) =>
      string[0] ? `${string[0].toUpperCase()}${string.substring(1)}` : "";
    facetName = facetName.trim();
    facetName = capitalizeFirstLetter(facetName);

    facetName = facetName.replace(/_(es|fr|en)$/, "");

    return facetName;
  }

  function addFilter(name, value) {
    const newFilters = props.filters.concat({ field: name, value: value });
    props.setFilters(newFilters);
  }

  function removeFilter(filter) {
    const newFilters = props.filters.filter(
      (item) => item.value !== filter.value
    );
    props.setFilters(newFilters);
  }

  function getFacetsByLanguage() {
    let newFacets = {};

    for (let prop in props.facets) {
      if (
        props.facets.hasOwnProperty(prop) &&
        prop.endsWith("_" + selectedLanguage)
      ) {
        newFacets[prop] = props.facets[prop].filter(function (item) {
          return !item.value.endsWith(selectedLanguage);
        });
      }
    }

    return newFacets;
  }

  let newFacets = getFacetsByLanguage();

  var facets;
  try {
    facets = Object.keys(newFacets).map((key) => {
      return (
        <CheckboxFacet
          key={key}
          name={key}
          values={newFacets[key]}
          addFilter={addFilter}
          removeFilter={removeFilter}
          mapFacetName={mapFacetName}
          selectedFacets={props.filters.filter((f) => f.field === key)}
        />
      );
    });
  } catch (error) {
    console.log(error);
  }

  const filters = props.filters.map((filter, index) => {
    return (
      <li key={index}>
        <Chip
          label={`${mapFacetName(filter.field)}: ${filter.value}`}
          onDelete={() => removeFilter(filter)}
          className="chip"
        />
      </li>
    );
  });

  return (
    <div id="facetPanel" className="box">
      <div className="facetbox">
        <div id="clearFilters">
          <ul className="filterlist">{filters}</ul>
        </div>
        <FacetList component="nav" className="listitem">
          {facets}
        </FacetList>
      </div>
    </div>
  );
}

const FacetList = styled(List)({
  marginTop: "32px !important",
});
