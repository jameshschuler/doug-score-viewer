<template>
  <div class="p-3">
    <h1 class="is-size-3 mb-3">Featured</h1>
    <div class="columns is-mobile card-list outer">
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
    </div>
    <i class="fas fa-chevron-right"></i>
    <div>{{ loading }}</div>
  </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import Card from "./Card.vue";

let loading = ref(true);

async function loadFeatured() {
  setTimeout(() => {
    loading.value = false;
  }, 1000);
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
