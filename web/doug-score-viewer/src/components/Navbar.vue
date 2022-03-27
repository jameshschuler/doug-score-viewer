<template>
  <nav class="navbar is-flex is-justify-content-space-between is-align-items-center" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
      <span class="navbar-item has-text-white is-clickable" @click="store.toggleSearchDrawer()">
        <i class="fas fa-lg fa-fw fa-search"></i>
      </span>
      <router-link to="/" id="home" class="navbar-item is-size-3 has-text-white has-text-weight-semibold">DougScore Viewer</router-link>
    </div>
    <router-link to="/about" class="navbar-item" id="navbar-item-about">
      <i class="fa-solid fa-2x fa-fw fa-circle-info"></i>
    </router-link>
  </nav>
  <SearchDrawer :class="{ 'slideout-open': store.isSearchDrawerOpen }" />
</template>
<script setup lang="ts">
import { store } from "../store";
import SearchDrawer from "./SearchDrawer.vue";
</script>
<style lang="scss" scoped>
@import "bulma/sass/utilities/mixins.sass";

.navbar {
  background-color: hsl(129, 45%, 57%);
}

#home {
  &:hover,
  &:focus {
    color: hsl(0, 0%, 100%) !important;
  }
}

#navbar-item-about {
  color: hsl(0, 0%, 100%);

  &:hover {
    background-color: hsl(171, 100%, 41%);
  }

  @include mobile {
    svg {
      font-size: 1.25em;
    }
  }
}
</style>
