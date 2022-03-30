<template>
  <div>
    <div class="columns">
      <div class="column is-10 is-offset-1">
        <div class="p-3">
          <div id="header">
            <h1 class="is-size-3" v-html="searchResultsText" v-if="!isNullEmptyOrWhitespace(currentSearchQueryDisplay)"></h1>
            <div class="mt-2 mb-4" v-if="currentSelectedCountries.length !== 0">
              <p><b>Including from:</b></p>
              <Flag class="mr-2" :country="country" v-for="country in currentSelectedCountries" />
            </div>
          </div>

          <div class="mt-4" id="search-results">
            <div
              class="is-flex is-justify-content-flex-end is-flex-direction-column is-align-items-flex-end mb-4"
              v-if="!isNullEmptyOrWhitespace(currentSearchQueryDisplay)"
            >
              <button @click="copyUrlToClipboard" class="button is-info is-light is-medium my-2">Share Results!</button>
              <div class="field">
                <div class="control is-expanded">
                  <div class="select is-fullwidth">
                    <select v-model="currentSortByOption" @change="updateSortByOption(($event.target! as HTMLSelectElement).value)">
                      <option v-for="option in sortByOptions" :value="option.value">
                        {{ option.text }}
                      </option>
                    </select>
                  </div>
                </div>
              </div>
            </div>
            <div v-if="!appError" class="columns is-flex-wrap-wrap">
              <Card v-for="dougScore in store.searchResults?.dougScores" :key="dougScore.id" :doug-score="dougScore" />
            </div>
            <div v-if="appError">
              <Notification :dismissible="false" :appError="appError" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { computed, ref } from "vue";
import { useRoute } from "vue-router";
import Card from "../components/Card.vue";
import Flag from "../components/Flag.vue";
import Notification from "../components/Notification.vue";
import { messages } from "../constants";
import { SortBy, sortByOptions } from "../constants/sortOptions";
import { AppError } from "../models/common";
import { AppErrorType } from "../models/enums/error";
import { store } from "../store";
import { isNullEmptyOrWhitespace } from "../utils/strings";

const route = useRoute();

const appError = computed(() => {
  if (store.searchResults === null || store.searchResults?.dougScores.length === 0) {
    return {
      errorType: AppErrorType.NotFound,
      message: messages.noSearchResults,
    } as AppError;
  }

  return null;
});

const currentSortByOption = ref<string>(store.getCurrentSortByOption());

const currentSelectedCountries = computed(() => {
  const { currentCountries } = store;
  return currentCountries && currentCountries.length !== 0 ? currentCountries : [];
});

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

const searchResultsText = computed(() => `Showing ${store.searchResults?.currentCount} DougScores for "<b>${currentSearchQueryDisplay.value}</b>"`);

function copyUrlToClipboard() {
  const fullUrl = `${window.location.origin}${route.fullPath}`;
  navigator.clipboard.writeText(fullUrl);

  // TODO: display toast
}

function updateSortByOption(sortByValue: string) {
  // TODO: Perform search?
  store.setSortByOption(sortByValue as SortBy);
}
</script>
<style lang="scss" scoped>
@import "bulma/sass/utilities/mixins.sass";

@include mobile {
  .field {
    width: 100%;
  }
}
</style>
