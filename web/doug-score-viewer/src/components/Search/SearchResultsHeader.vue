<template>
  <div id="header" class="mb-5">
    <div class="is-flex is-justify-content-space-between is-align-items-center mb-1">
      <h1 class="is-size-3" v-html="searchResultsText" v-if="!isNullEmptyOrWhitespace(currentSearchQueryDisplay)"></h1>
      <button v-if="hasResults" @click="copyUrlToClipboard" class="button is-info is-light is-medium my-2">Share Results!</button>
    </div>
    <div v-if="currentSelectedCountries.length !== 0">
      <p><b>Including from:</b></p>
      <Flag class="mr-2" :country="country" v-for="country in currentSelectedCountries" />
    </div>
  </div>
</template>
<script setup lang="ts">
import { computed } from "vue";
import { useRoute } from "vue-router";
import { store } from "../../store";
import { isNullEmptyOrWhitespace } from "../../utils/strings";
import Flag from "../Flag.vue";

const route = useRoute();

function copyUrlToClipboard() {
  const fullUrl = `${window.location.origin}${route.fullPath}`;
  navigator.clipboard.writeText(fullUrl);

  // TODO: display toast
}

const hasResults = computed(() => store.searchResults !== null && store.searchResults.dougScores.length !== 0);
const currentSelectedCountries = computed(() => {
  const { currentCountries } = store;
  return currentCountries && currentCountries.length !== 0 ? currentCountries : [];
});

const searchResultsText = computed(
  () => `Showing ${store.searchResults?.currentCount ?? 0} DougScores for "<b>${currentSearchQueryDisplay.value}</b>"`
);

const currentSearchQueryDisplay = computed(() => {
  const { currentSearchQuery } = store;
  if (currentSearchQuery) {
    const { make, model, minYear, maxYear } = currentSearchQuery;
    let message = `${minYear}-${maxYear}`;

    message += !isNullEmptyOrWhitespace(make) || !isNullEmptyOrWhitespace(model) ? ", " : "";
    message += `${make} ${model}`.trim();

    return message;
  }

  return null;
});
</script>
<style lang="scss"></style>
