<template>
  <nav class="navbar has-background-primary" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
      <span class="navbar-item has-text-white is-clickable" @click="toggleSearchDrawer()">
        <i class="fas fa-lg fa-fw fa-search"></i>
      </span>
      <router-link to="/" class="navbar-item is-size-3 has-text-white">DougScore Viewer</router-link>
    </div>
  </nav>
  <SearchDrawer :class="{ 'slideout-open': showingSearchDrawer }" :toggle-search-drawer="toggleSearchDrawer" />
</template>
<script setup lang="ts">
import { ref } from "vue";
import SearchDrawer from "./SearchDrawer.vue";

const showingSearchDrawer = ref(false);

function toggleSearchDrawer() {
  showingSearchDrawer.value = !showingSearchDrawer.value;
}
</script>
<style lang="scss"></style>
