<template>
  <div id="header" class="mb-5">
    <div class="is-flex is-justify-content-space-between is-align-items-center mb-1">
      <h1 class="is-size-3 is-size-4-mobile" v-html="searchResultsMessage"></h1>
      <button v-if="hasResults" @click="copyUrlToClipboard" class="button is-info is-light is-medium">Share Results!</button>
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

const searchResultsMessage = computed(() => {
  if (store.currentSearchQuery === null) {
    return "";
  }

  const count = store.searchResults?.currentCount ?? 0;
  let message = `Showing ${count} DougScores between `;
  const { make, model, minYear, maxYear } = store.currentSearchQuery;

  message += `<span class="has-text-weight-medium">${minYear}</span> and <span class="has-text-weight-medium">${maxYear}</span>`;

  if (!isNullEmptyOrWhitespace(make)) {
    message += ` for <span class="has-text-weight-medium">${make}</span>`;
  }

  if (!isNullEmptyOrWhitespace(model)) {
    message += !isNullEmptyOrWhitespace(make) ? "" : " for";
    message += ` <span class="has-text-weight-medium">${model}</span>`;
  }

  return message;
});
</script>
<style lang="scss" scoped>
@import "bulma/sass/utilities/mixins.sass";

#header {
  div:first-child {
    @include mobile {
      flex-direction: column-reverse;
      align-items: stretch;

      button {
        margin-bottom: 1rem;
      }
    }
  }
}
</style>
