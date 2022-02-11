<template>
  <div class="p-3">
    <h1 class="is-size-3 mb-3">Highest DougScores</h1>
    <div v-if="!loading && !appError" class="columns is-mobile card-list outer">
      <Card v-for="dougScore in dougScores" :doug-score="dougScore" />
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
import Notification from "../components/Notification.vue";
import { AppError } from "../models/common";
import { DougScoreResponse } from "../models/dougScore";
import { getHighestDougScores } from "../services/dougScoreService";
import { NotificationType } from "../models/enums/notification";

const appError = ref<AppError>();
const loading = ref(true);
const dougScores = ref<DougScoreResponse[]>([]);

async function loadHighestDougScores() {
  loading.value = true;

  const { data, error } = await getHighestDougScores();

  if (!error) {
    dougScores.value = data!.dougScores;
  } else {
    appError.value = error;
  }
  loading.value = false;
}

loadHighestDougScores();
</script>
<style lang="scss"></style>
