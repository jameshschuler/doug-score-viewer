<template>
  <div class="p-3">
    <h1 class="is-size-3 mb-3">Featured</h1>
    <div v-if="!loading" class="columns is-mobile card-list outer">
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
    </div>
    <LoadingIndicator v-if="loading" />
  </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import Card from "./Card.vue";
import LoadingIndicator from "./LoadingIndicator.vue";

const loading = ref(true);

async function loadFeatured() {
  setTimeout(() => {
    loading.value = false;
  }, 3000);
}

loadFeatured();
</script>
<style lang="scss">
.outer {
  overflow-x: scroll;
  overflow-y: hidden;
  -webkit-overflow-scrolling: touch;
  -ms-overflow-style: none; /* IE and Edge */
  scrollbar-width: none; /* Firefox */

  &::-webkit-scrollbar {
    display: none;
  }
}
</style>
