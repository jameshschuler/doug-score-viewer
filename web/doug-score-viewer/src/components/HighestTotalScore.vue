<template>
  <div class="p-3">
    <h1 class="is-size-3 mb-3">Highest DougScores</h1>
    <div v-if="!loading" class="columns is-mobile card-list outer">
      <!-- <Card />
      <Card />
      <Card />
      <Card />
      <Card /> -->
    </div>
    <LoadingIndicator v-if="loading" />
  </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import Card from "./Card.vue";
import LoadingIndicator from "./LoadingIndicator.vue";

const loading = ref(true);

async function loadHighestDougScores() {
  setTimeout(() => {
    loading.value = false;
  }, 1000);
}

loadHighestDougScores();
</script>
<style lang="scss"></style>
