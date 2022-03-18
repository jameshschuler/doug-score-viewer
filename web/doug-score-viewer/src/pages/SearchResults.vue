<template>
  <div>
    <div class="columns">
      <div class="column box is-10 is-offset-1">
        <div class="p-3">
          <h1 class="is-size-3 mb-4" v-if="!isNullEmptyOrWhitespace(currentSearchQueryDisplay)">
            Showing {{ store.searchResults?.currentCount }} DougScores for "<b>{{ currentSearchQueryDisplay }}</b
            >"
          </h1>
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

const currentSearchQueryDisplay = computed(() => {
  const { currentSearchQuery } = store;
  if (currentSearchQuery) {
    const { make, model, minYear, maxYear, originCountries } = currentSearchQuery;
    let message = `${minYear}-${maxYear}`;

    message += !isNullEmptyOrWhitespace(make) || !isNullEmptyOrWhitespace(model) ? ", " : "";
    message += `${make} ${model}`.trim();

    const selectedCountries = originCountries.filter((c) => c.selected);
    if (selectedCountries.length !== 0) {
      // TODO: idk what this should look like
      //message += ` ${selectedCountries[0].name}`;
    }

    return message;
  }

  return null;
});
</script>
<style lang="scss"></style>
