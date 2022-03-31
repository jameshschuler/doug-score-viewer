<template>
  <div class="mt-4" id="search-results">
    <SortByDropdown />

    <div v-if="hasResults" class="columns is-flex-wrap-wrap">
      <Card v-for="dougScore in store.searchResults?.dougScores" :key="dougScore.id" :doug-score="dougScore" />
    </div>
    <Notification :dismissible="false" :appError="appError" v-if="appError" />
    <Notification :dismissible="false" :appError="noSearchMessage" v-if="noSearchMessage" />
  </div>
</template>
<script setup lang="ts">
import { computed } from "vue";
import { messages } from "../../constants";
import { AppError } from "../../models/common";
import { AppErrorType } from "../../models/enums/error";
import { store } from "../../store";
import Card from "../Card.vue";
import Notification from "../Notification.vue";
import SortByDropdown from "./SortByDropdown.vue";

const hasResults = computed(() => store.searchResults !== null && store.searchResults.dougScores.length !== 0);
const appError = computed(() => store.error);
const noSearchMessage = computed(() => {
  if (store.currentSearchQuery === null) {
    return {
      errorType: AppErrorType.NotFound,
      message: messages.noSearch,
    } as AppError;
  }

  return null;
});
</script>
<style lang="scss" scoped>
@import "bulma/sass/utilities/mixins.sass";

@include mobile {
  .field {
    width: 100%;
  }
}
</style>
