<template>
  <div>
    <div class="columns">
      <div class="column box is-10 is-offset-1">
        <div class="p-3">
          <h1 class="is-size-3" v-if="!isNullEmptyOrWhitespace(currentSearchQueryDisplay)" v-html="searchResultsText"></h1>
          <div class="mt-2 mb-4">
            <p><b>Including:</b></p>
            <Flag class="mr-2" :country="country" v-for="country in currentSelectedCountries" />
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
</template>
<script setup lang="ts">
import { computed } from "vue";
import Card from "../components/Card.vue";
import Flag from "../components/Flag.vue";
import Notification from "../components/Notification.vue";
import messages from "../constants/messages";
import { AppError } from "../models/common";
import { AppErrorType } from "../models/enums/error";
import { store } from "../store";
import { isNullEmptyOrWhitespace } from "../utils/strings";

const appError = computed(() => {
  if (store.searchResults === null || store.searchResults?.dougScores.length === 0) {
    return {
      errorType: AppErrorType.NotFound,
      message: messages.noSearchResults,
    } as AppError;
  }

  return null;
});

// TODO: this shouldn't update when the state actually changes from the search drawer
// Maybe use watch on the store.currentSearchQuery.originCountries array
const currentSelectedCountries = computed(() => {
  const { currentSearchQuery } = store;
  if (currentSearchQuery && currentSearchQuery.originCountries.length !== 0) {
    return currentSearchQuery.originCountries.filter((c) => c.selected);
  }

  return [];
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
</script>
<style lang="scss"></style>
