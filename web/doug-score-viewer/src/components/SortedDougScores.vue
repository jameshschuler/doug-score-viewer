<template>
  <div class="p-4">
    <h1 class="is-size-3 mb-3">{{ props.title }}</h1>
    <div v-if="!loading && !appError" class="columns is-mobile card-list outer">
      <Card v-for="dougScore in dougScores" :doug-score="dougScore" :key="dougScore.id" />
    </div>
    <div v-if="!loading && appError">
      <Notification :dismissible="false" :appError="appError" />
    </div>
    <LoadingIndicator v-if="loading" />
  </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import Card from "./Card.vue";
import LoadingIndicator from "./LoadingIndicator.vue";
import Notification from "./Notification.vue";
import { AppError } from "../models/common";
import { DougScoreResponse } from "../models/dougScore";
import { getDougScores } from "../services/dougScoreService";

const props = defineProps({
  sortBy: String,
  title: String,
});

const appError = ref<AppError>();
const loading = ref(true);
const dougScores = ref<DougScoreResponse[]>([]);

async function loadDougScores() {
  loading.value = true;

  const { data, error } = await getDougScores(props.sortBy!);

  if (!error && data) {
    dougScores.value = data!.dougScores;
  } else {
    appError.value = error;
  }
  loading.value = false;
}

loadDougScores();
</script>
<style lang="scss"></style>
