<template>
  <div class="p-3 mb-3">
    <h1 class="is-size-3 mb-3 has-text-weight-medium">Featured</h1>
    <div v-if="!loading && !appError" class="columns is-mobile card-list outer">
      <Card v-for="dougScore in featuredDougScores" :key="dougScore.id" :doug-score="dougScore" size="is-two-fifths-desktop" />
    </div>
    <div v-if="!loading && appError">
      <Notification :dismissible="false" :appError="appError" />
    </div>
    <LoadingIndicator v-if="loading" />
  </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import Notification from "../components/Notification.vue";
import { AppError } from "../models/common";
import { DougScoreResponse } from "../models/dougScore";
import { getFeaturedDougScores } from "../services/dougScoreService";
import Card from "./Card.vue";
import LoadingIndicator from "./LoadingIndicator.vue";

const appError = ref<AppError>();
const loading = ref(true);
const featuredDougScores = ref<DougScoreResponse[]>([]);

async function loadFeatured() {
  loading.value = true;

  const { data, error } = await getFeaturedDougScores();

  if (!error) {
    featuredDougScores.value = data!.dougScores;
  } else {
    appError.value = error;
  }

  loading.value = false;
}

loadFeatured();
</script>
<style lang="scss"></style>
